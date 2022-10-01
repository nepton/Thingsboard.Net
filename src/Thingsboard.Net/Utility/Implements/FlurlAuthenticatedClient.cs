using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using Polly;
using Thingsboard.Net.DependencyInjection;
using Thingsboard.Net.Exceptions;
using Thingsboard.Net.Tokens;

namespace Thingsboard.Net.Utility;

public abstract class FlurlAuthenticatedClient<TClientApi> : IAuthenticatedClient<TClientApi> where TClientApi : IAuthenticatedClient<TClientApi>
{
    private string? _username;
    private string? _password;

    private readonly IAccessTokenCaching         _accessTokenStorage;
    private readonly ThingsboardNetOptions _options;

    protected FlurlAuthenticatedClient(
        IAccessTokenCaching                           accessTokenStorage,
        IOptionsSnapshot<ThingsboardNetOptions> options)
    {
        _accessTokenStorage = accessTokenStorage;
        _options            = options.Value;
    }

    public TClientApi WithAuthenticate(string username, string password)
    {
        _username = username ?? throw new ArgumentNullException(nameof(username));
        _password = password ?? throw new ArgumentNullException(nameof(password));

        return (TClientApi) (object) this;
    }

    protected async Task<IFlurlRequest> CreateRequest(string path, CancellationToken cancel)
    {
        if (path == null) throw new ArgumentNullException(nameof(path));

        var username = _username ?? _options.Username ?? throw new ArgumentNullException(nameof(_options.Username));
        var password = _password ?? _options.Password ?? throw new ArgumentNullException(nameof(_options.Password));
        var token    = await _accessTokenStorage.GetAccessTokenAsync(username, password, cancel);

        var baseUrl = _options.Url ?? throw new TbException("Thingsboard URL is not set");
        var url     = baseUrl.AppendPathSegment(path);

        return new FlurlRequest(url)
            .WithTimeout(_options.GetTimeoutInSecOrDefault())
            .WithOAuthBearerToken(token);
    }

    protected IAsyncPolicy CreatePolicy(params IAsyncPolicy[] policies)
    {
        var username           = _username ?? _options.Username ?? throw new ArgumentNullException(nameof(_options.Username));
        var tokenExpiredPolicy = _accessTokenStorage.GetTokenExpiredPolicy(username);

        if (policies.Any())
        {
            return Policy.WrapAsync(tokenExpiredPolicy, Policy.WrapAsync(policies));
        }

        return tokenExpiredPolicy;
    }
}
