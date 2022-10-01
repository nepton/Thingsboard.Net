using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Microsoft.Extensions.Options;
using Polly;
using Thingsboard.Net.DependencyInjection;
using Thingsboard.Net.Exceptions;
using Thingsboard.Net.TbAuth;

namespace Thingsboard.Net.Tokens;

public class InMemoryAccessTokenCaching : IAccessTokenCaching
{
    private readonly        ThingsboardNetOptions                          _options;
    private readonly        ITbAuth                                        _auth;
    private static readonly ConcurrentDictionary<string, AccessTokenModel> _tokens = new();

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public InMemoryAccessTokenCaching(ITbAuth   auth,
        IOptionsSnapshot<ThingsboardNetOptions> options)
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

    public async Task<string> GetAccessTokenAsync(string username, string password, CancellationToken cancel)
    {
        if (_tokens.TryGetValue(username, out var model))
        {
            if (model.ExpiresAt > DateTime.Now)
            {
                return model.AccessToken;
            }
        }

        var response = await _auth.LoginAsync(new TbLoginRequest(username, password), cancel);
        if (response.Token is not {Length: > 0} token)
            throw new TbException("Failed to get access token");

        model = new AccessTokenModel
        {
            Username    = username,
            Password    = password,
            AccessToken = response.Token,
            ExpiresAt   = DateTime.Now.AddSeconds(_options.GetDynamicTokenExpiresInSec())
        };

        _tokens.AddOrUpdate(username, model, (_, _) => model);
        return model.AccessToken;
    }
}
