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

public class RequestPolicyBuilder
{
    private readonly ThingsboardNetFlurlOptionsReader _options;
    private readonly ILogger<RequestPolicyBuilder>    _logger;
    private readonly List<IAsyncPolicy>               _policies = new();
    private readonly IRequestBuilder                  _requestBuilder;

    public RequestPolicyBuilder(ThingsboardNetFlurlOptionsReader options, ILogger<RequestPolicyBuilder> logger, IRequestBuilder requestBuilder)
    {
        _options        = options;
        _logger         = logger;
        _requestBuilder = requestBuilder;
    }

    public RequestPolicyBuilder RetryOnUnauthorized()
    {
        var policy = Policy.Handle<TbHttpException>(x => x.StatusCode == HttpStatusCode.Unauthorized)
            .RetryAsync(1,
                (_, retryCount) =>
                {
                    _logger.LogWarning("Unauthorized request. Retrying {RetryCount} time", retryCount);
                });

        _policies.Add(policy);
        return this;
    }

    public RequestPolicyBuilder RetryOnHttpTimeout()
    {
        return RetryOnHttpTimeout(_options.RetryTimes, _options.RetryIntervalInSec);
    }

    public RequestPolicyBuilder RetryOnHttpTimeout(int retryTimes, int retryWaitInSec)
    {
        var policy = Policy.Handle<FlurlHttpTimeoutException>()
            .WaitAndRetryAsync(retryTimes,
                _ => TimeSpan.FromSeconds(retryWaitInSec),
                (_, retryCount) =>
                {
                    _logger.LogWarning("Request timeout. Retrying {RetryCount} time", retryCount);
                });

        _policies.Add(policy);
        return this;
    }

    public RequestPolicyBuilder FallbackOn(HttpStatusCode statusCode, Func<Task> fallbackAsync)
    {
        if (fallbackAsync == null) throw new ArgumentNullException(nameof(fallbackAsync));
        var policy = Policy.Handle<TbHttpException>(x => x.StatusCode == statusCode)
            .FallbackAsync(_ => fallbackAsync());

        _policies.Add(policy);

        return this;
    }

    public IAsyncRequestPolicy Build()
    {
        if (_policies.Count == 0)
            return new AsyncRequestPolicy(_requestBuilder, Policy.NoOpAsync());

        if (_policies.Count == 1)
            return new AsyncRequestPolicy(_requestBuilder, _policies[0]);

        return new AsyncRequestPolicy(_requestBuilder, Policy.WrapAsync(_policies.ToArray()));
    }
}