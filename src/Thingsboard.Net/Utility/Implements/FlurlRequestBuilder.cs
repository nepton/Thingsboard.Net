using System;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.DependencyInjection;
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
    private readonly ILogger<FlurlRequestBuilder> _logger;
    private readonly IServiceProvider             _serviceProvider;

    public FlurlRequestBuilder(
        IOptionsSnapshot<ThingsboardNetOptions> options,
        ILogger<FlurlRequestBuilder>            logger,
        IServiceProvider                        serviceProvider)
    {
        _logger          = logger;
        _serviceProvider = serviceProvider;
        _defaultOptions  = options.Value;
    }

    /// <summary>
    /// Create a new request builder for the specified URL without any authentication
    /// </summary>
    /// <param name="path"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="TbException"></exception>
    /// <exception cref="TbHttpException"></exception>
    public IFlurlRequest CreateAnonymousRequest(string path, TbClientApiOptions options)
    {
        if (path == null) throw new ArgumentNullException(nameof(path));
        if (options == null) throw new ArgumentNullException(nameof(options));

        var baseUrl = _defaultOptions.Url ?? throw new TbException("Thingsboard URL is not set");
        var url     = baseUrl.AppendPathSegment(path);

        var flurl = new FlurlRequest(url)
            .WithTimeout(TimeSpan.FromSeconds(options.TimeoutInSec ?? _defaultOptions.TimeoutInSec ?? 10))
            .ConfigureRequest(action =>
            {
                action.OnErrorAsync = async (call) =>
                {
                    var error = await call.Response.GetJsonAsync<TbResponseFault>();
                    throw new TbHttpException(error);
                };
            });

        return flurl;
    }

    /// <summary>
    /// Creates a new Flurl request with the specified path, options, and access token.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="options"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="TbException"></exception>
    /// <exception cref="TbHttpException"></exception>
    public async Task<IFlurlRequest> CreateRequest(string path, TbClientApiOptions options, CancellationToken cancel)
    {
        if (path == null) throw new ArgumentNullException(nameof(path));
        if (options == null) throw new ArgumentNullException(nameof(options));

        var baseUrl = _defaultOptions.Url ?? throw new TbException("Thingsboard URL is not set");
        var url     = baseUrl.AppendPathSegment(path);

        var accessTokenService = _serviceProvider.GetRequiredService<IAccessToken>();
        var credentials        = options.Credentials ?? _defaultOptions.GetDefaultCredentials();
        var accessToken        = await accessTokenService.GetAccessTokenAsync(credentials, cancel);

        var flurl = new FlurlRequest(url)
            .WithTimeout(TimeSpan.FromSeconds(options.TimeoutInSec ?? _defaultOptions.TimeoutInSec ?? 10))
            .WithOAuthBearerToken(accessToken)
            .ConfigureRequest(action =>
            {
                action.OnErrorAsync = async (call) =>
                {
                    if (call.Response.StatusCode == 401)
                        await accessTokenService.RemoveExpiredTokenAsync(credentials);

                    var error = await call.Response.GetJsonAsync<TbResponseFault>();
                    throw new TbHttpException(error);
                };
            });
        return flurl;
    }

    /// <summary>
    /// Create a new FlurlRequest with a retry policy
    /// </summary>
    /// <returns></returns>
    public IAsyncPolicy CreateAnonymousPolicy()
    {
        var timeoutPolicy = Policy.Handle<FlurlHttpTimeoutException>()
            .WaitAndRetryAsync(3,
                _ => TimeSpan.FromSeconds(1),
                (exception, span, retryCount) => _logger.LogWarning(exception, "Timeout. Retry {RetryCount} in {Span}", retryCount, span));

        return timeoutPolicy;
    }

    /// <summary>
    /// Create a policy for requests with access token
    /// </summary>
    /// <returns></returns>
    public IAsyncPolicy CreatePolicy()
    {
        var reAuthPolicy = Policy.Handle<TbHttpException>(x => x.Status == 401)
            .RetryAsync(1);

        var timeoutPolicy = Policy.Handle<FlurlHttpTimeoutException>()
            .WaitAndRetryAsync(3,
                _ => TimeSpan.FromSeconds(1),
                (exception, span, retryCount) => _logger.LogWarning(exception, "Timeout. Retry {RetryCount} in {Span}", retryCount, span));

        return Policy.WrapAsync(reAuthPolicy, timeoutPolicy);
    }
}
