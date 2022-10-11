using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Flurl.Http;
using Polly;
using Thingsboard.Net.Exceptions;
using Thingsboard.Net.Flurl.Options;

namespace Thingsboard.Net.Flurl.Utilities;

public class RequestPolicyBuilder<TResult>
{
    private readonly ThingsboardNetFlurlOptions  _options;
    private readonly List<IAsyncPolicy<TResult>> _policies = new();

    public RequestPolicyBuilder(ThingsboardNetFlurlOptions options)
    {
        _options = options;
    }

    public RequestPolicyBuilder<TResult> RetryOnUnauthorized()
    {
        var policy = Policy<TResult>.Handle<TbHttpException>(x => x.StatusCode == HttpStatusCode.Unauthorized)
            .RetryAsync(1);

        _policies.Add(policy);
        return this;
    }

    public RequestPolicyBuilder<TResult> RetryOnHttpTimeout()
    {
        return RetryOnHttpTimeout(_options.TimeoutRetryTimes ?? 3, _options.TimeoutRetryWaitInSec ?? 1);
    }

    public RequestPolicyBuilder<TResult> RetryOnHttpTimeout(int retryTimes, int retryWaitInSec)
    {
        var policy = Policy<TResult>.Handle<FlurlHttpTimeoutException>()
            .WaitAndRetryAsync(retryTimes, _ => TimeSpan.FromSeconds(retryWaitInSec));

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

    public IAsyncPolicy<TResult> Build()
    {
        if (_policies.Count == 0)
            return Policy.NoOpAsync<TResult>();

        if (_policies.Count == 1)
            return _policies[0];

        return Policy.WrapAsync(_policies.ToArray());
    }
}

public class RequestPolicyBuilder
{
    private readonly ThingsboardNetFlurlOptions _options;
    private readonly List<IAsyncPolicy>         _policies = new();

    public RequestPolicyBuilder(ThingsboardNetFlurlOptions options)
    {
        _options = options;
    }

    public RequestPolicyBuilder RetryOnUnauthorized()
    {
        var policy = Policy.Handle<TbHttpException>(x => x.StatusCode == HttpStatusCode.Unauthorized)
            .RetryAsync(1);

        _policies.Add(policy);
        return this;
    }

    public RequestPolicyBuilder RetryOnHttpTimeout()
    {
        return RetryOnHttpTimeout(_options.TimeoutRetryTimes ?? 3, _options.TimeoutRetryWaitInSec ?? 1);
    }

    public RequestPolicyBuilder RetryOnHttpTimeout(int retryTimes, int retryWaitInSec)
    {
        var policy = Policy.Handle<FlurlHttpTimeoutException>()
            .WaitAndRetryAsync(retryTimes, _ => TimeSpan.FromSeconds(retryWaitInSec));

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

    public IAsyncPolicy Build()
    {
        if (_policies.Count == 0)
            return Policy.NoOpAsync();

        if (_policies.Count == 1)
            return _policies[0];

        return Policy.WrapAsync(_policies.ToArray());
    }
}
