using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using Polly;
using Thingsboard.Net.Exceptions;
using Thingsboard.Net.Flurl.Options;

namespace Thingsboard.Net.Flurl.Utilities;

public class RequestPolicyBuilder<TResult>
{
    private readonly ThingsboardNetFlurlOptionsReader       _options;
    private readonly ILogger<RequestPolicyBuilder<TResult>> _logger;

    private readonly List<IAsyncPolicy<TResult>> _policies = new();
    private readonly IRequestBuilder             _requestBuilder;

    public RequestPolicyBuilder(
        ThingsboardNetFlurlOptionsReader       options,
        ILogger<RequestPolicyBuilder<TResult>> logger,
        IRequestBuilder                        requestBuilder)
    {
        _options        = options;
        _logger         = logger;
        _requestBuilder = requestBuilder;
    }

    public RequestPolicyBuilder<TResult> RetryOnUnauthorized()
    {
        var policy = Policy<TResult>.Handle<TbHttpException>(x => x.StatusCode == HttpStatusCode.Unauthorized)
            .RetryAsync(1,
                (_, retryCount, _) =>
                {
                    _logger.LogWarning("Unauthorized request. Retrying {RetryCount} time", retryCount);
                });

        _policies.Add(policy);
        return this;
    }

    public RequestPolicyBuilder<TResult> RetryOnHttpTimeout()
    {
        return RetryOnHttpTimeout(_options.RetryTimes, _options.RetryIntervalInSec);
    }

    public RequestPolicyBuilder<TResult> RetryOnHttpTimeout(int retryTimes, int retryWaitInSec)
    {
        var policy = Policy<TResult>.Handle<FlurlHttpTimeoutException>()
            .WaitAndRetryAsync(retryTimes,
                _ => TimeSpan.FromSeconds(retryWaitInSec),
                (_, retryCount, _) =>
                {
                    _logger.LogWarning("Request timeout. Retrying {RetryCount} time", retryCount);
                });

        _policies.Add(policy);
        return this;
    }

    public RequestPolicyBuilder<TResult> FallbackValueOn(HttpStatusCode statusCode, TResult fallbackValue)
    {
        var policy = Policy<TResult>.Handle<TbHttpException>(x => x.StatusCode == statusCode)
            .FallbackAsync(fallbackValue);

        _policies.Add(policy);
        return this;
    }

    public RequestPolicyBuilder<TResult> FallbackOn(HttpStatusCode statusCode, Func<Task<TResult>> fallbackAsync)
    {
        if (fallbackAsync == null) throw new ArgumentNullException(nameof(fallbackAsync));
        var policy = Policy<TResult>.Handle<TbHttpException>(x => x.StatusCode == statusCode)
            .FallbackAsync(_ => fallbackAsync());

        _policies.Add(policy);

        return this;
    }

    public IAsyncRequestPolicy<TResult> Build()
    {
        if (_policies.Count == 0)
            return new AsyncRequestPolicy<TResult>(_requestBuilder, Policy.NoOpAsync<TResult>());

        if (_policies.Count == 1)
            return new AsyncRequestPolicy<TResult>(_requestBuilder, _policies[0]);

        return new AsyncRequestPolicy<TResult>(_requestBuilder, Policy.WrapAsync(_policies.ToArray()));
    }
}
