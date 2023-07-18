using System;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Thingsboard.Net.Exceptions;
using Thingsboard.Net.Flurl.Utilities.Models;

namespace Thingsboard.Net.Flurl.Utilities;

public class InMemoryAccessTokenRepository : IAccessTokenRepository
{
    /// <summary>
    /// Token cache
    /// </summary>
    private static readonly ConcurrentDictionary<string, AccessTokenModel> _tokens = new();

    /// <summary>
    /// Retrieve the token from the cache or from the server
    /// </summary>
    /// <param name="credentials"></param>
    /// <param name="accessTokenCaller"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<string> GetOrAddTokenAsync(TbCredentials credentials, Func<CancellationToken, Task<TbLoginToken>> accessTokenCaller, CancellationToken cancel)
    {
        if (credentials == null) throw new ArgumentNullException(nameof(credentials));
        if (accessTokenCaller == null) throw new ArgumentNullException(nameof(accessTokenCaller));

        if (_tokens.TryGetValue(credentials.Username, out var model))
        {
            if (model.ExpiresAt > DateTime.Now)
            {
                return model.AccessToken;
            }
        }

        // Get the token from the server
        var response = await accessTokenCaller(cancel);
        if (response.Token is not {Length: > 0})
            throw new TbAuthorizationException("Invalid token");

        var handler = new JwtSecurityTokenHandler();
        var token   = handler.ReadJwtToken(response.Token) ?? throw new TbAuthorizationException("Cannot read token");

        model = new AccessTokenModel
        {
            Username     = credentials.Username,
            Password     = credentials.Password,
            AccessToken  = response.Token,
            RefreshToken = response.RefreshToken ?? throw new TbAuthorizationException("Invalid refresh token"),
            Issuer       = token.Issuer,
            IssuedAt     = token.IssuedAt,
            ExpiresAt    = token.ValidTo,
            UserId       = token.Claims.FirstOrDefault(c => c.Type == "userId")?.Value?.ToGuid(),
            Scopes       = token.Claims.Where(c => c.Type == "scopes").Select(c => c.Value).ToArray(),
            Enabled      = token.Claims.FirstOrDefault(c => c.Type == "enabled")?.Value?.ToBoolean(),
            IsPublic     = token.Claims.FirstOrDefault(c => c.Type == "isPublic")?.Value?.ToBoolean(),
            TenantId     = token.Claims.FirstOrDefault(c => c.Type == "tenantId")?.Value?.ToGuid(),
            CustomerId   = token.Claims.FirstOrDefault(c => c.Type == "customerId")?.Value?.ToGuid(),
        };

        _tokens.AddOrUpdate(credentials.Username, model, (_, _) => model);
        return model.AccessToken;
    }

    /// <summary>
    /// Clear the token from the cache
    /// </summary>
    /// <returns></returns>
    public Task RemoveExpiredTokenAsync(TbCredentials credentials)
    {
        if (credentials == null) throw new ArgumentNullException(nameof(credentials));

        _tokens.TryRemove(credentials.Username, out _);
        return Task.CompletedTask;
    }
}
