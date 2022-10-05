using System;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Thingsboard.Net.DependencyInjection;
using Thingsboard.Net.Exceptions;
using Thingsboard.Net.Models;

namespace Thingsboard.Net.Utility;

public class FlurlRequestBuilder : IRequestBuilder
{
    private readonly ThingsboardNetOptions        _defaultOptions;
    private readonly IAccessToken                 _accessTokenCaching;
    private readonly ILogger<FlurlRequestBuilder> _logger;

    public FlurlRequestBuilder(IOptionsSnapshot<ThingsboardNetOptions> options, IAccessToken accessTokenCaching, ILogger<FlurlRequestBuilder> logger)
    {
        _accessTokenCaching = accessTokenCaching;
        _logger             = logger;
        _defaultOptions     = options.Value;
    }

    public async Task<IFlurlRequest> CreateRequest(string path, TbClientApiOptions options, CancellationToken cancel)
    {
        if (path == null) throw new ArgumentNullException(nameof(path));
        if (options == null) throw new ArgumentNullException(nameof(options));

        var baseUrl = _defaultOptions.Url ?? throw new TbException("Thingsboard URL is not set");
        var url     = baseUrl.AppendPathSegment(path);

        var credentials = options.Credentials ?? _defaultOptions.GetDefaultCredentials();
        var accessToken = await _accessTokenCaching.GetAccessTokenAsync(credentials, cancel);

        var flurl = new FlurlRequest(url)
            .WithTimeout(TimeSpan.FromSeconds(options.TimeoutInSec ?? _defaultOptions.TimeoutInSec ?? 10))
            .WithOAuthBearerToken(accessToken)
            .ConfigureRequest(action =>
            {
                action.OnErrorAsync = async (call) =>
                {
                    if (call.Response.StatusCode == 401)
                        await _accessTokenCaching.RemoveExpiredTokenAsync(credentials);

                    var error = await call.Response.GetJsonAsync<TbResponseFault>();
                    throw new TbHttpException(error);
                };
            });

        return flurl;
    }

    public IAsyncPolicy CreatePolicy()
    {
        var reAuthPolicy = Policy.Handle<TbHttpException>(x => x.Status == 401)
            .RetryAsync(1);

        var timeoutPolicy = Policy.Handle<FlurlHttpTimeoutException>()
            .WaitAndRetryAsync(3,
                _ => TimeSpan.FromSeconds(1),
                (exception, span, retryCount) => _logger.LogWarning(exception, "Timeout. Retry {retryCount} in {span}", retryCount, span));

        return Policy.WrapAsync(reAuthPolicy, timeoutPolicy);
    }
}
