using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Microsoft.Extensions.Options;
using Polly;
using Thingsboard.Net.Exceptions;
using Thingsboard.Net.Flurl.Options;
using Thingsboard.Net.Flurl.Utilities.Models;

namespace Thingsboard.Net.Flurl.Utilities.Implements;

public class InMemoryCachedAccessToken : IAccessToken
{
    private readonly ThingsboardNetFlurlOptions _options;
    private readonly ITbLoginClient             _auth;

    /// <summary>
    /// Token cache
    /// </summary>
    private static readonly ConcurrentDictionary<string, AccessTokenModel> _tokens = new();

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public InMemoryCachedAccessToken(ITbLoginClient  auth,
        IOptionsSnapshot<ThingsboardNetFlurlOptions> options)
    {
        _auth    = auth;
        _options = options.Value;
    }

    public IAsyncPolicy GetTokenExpiredPolicy(string username)
    {
        return Policy
            .Handle<FlurlHttpException>(ex => ex.StatusCode == 401)
            .RetryAsync(1,
                (_, _) =>
                {
                    _tokens.TryRemove(username, out _);
                    return Task.CompletedTask;
                });
    }

    /// <summary>
    /// Retrieve the token from the cache or from the server
    /// </summary>
    /// <param name="credentials"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<string> GetAccessTokenAsync(TbCredentials credentials, CancellationToken cancel)
    {
        if (credentials == null) throw new ArgumentNullException(nameof(credentials));

        if (_tokens.TryGetValue(credentials.Username, out var model))
        {
            if (model.ExpiresAt > DateTime.Now)
            {
                return model.AccessToken;
            }
        }

        var response = await _auth.LoginAsync(new TbLoginRequest(credentials.Username, credentials.Password), cancel);
        if (response.Token is not {Length: > 0})
            throw new TbException("Failed to get access token");

        model = new AccessTokenModel
        {
            Username    = credentials.Username,
            Password    = credentials.Password,
            AccessToken = response.Token,
            ExpiresAt   = DateTime.Now.AddSeconds(_options.DynamicTokenExpiresInSec ?? 3600)
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
