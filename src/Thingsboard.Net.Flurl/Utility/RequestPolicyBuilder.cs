using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Flurl.Http;
using Polly;
using Thingsboard.Net.Exceptions;

namespace Thingsboard.Net.Flurl.Utility;

public class RequestPolicyBuilder
{
    private readonly List<IAsyncPolicy> _policies = new();

    public RequestPolicyBuilder RetryOnUnauthorized()
    {
        var policy = Policy.Handle<TbHttpException>(x => x.StatusCode == HttpStatusCode.Unauthorized)
            .RetryAsync(1);

        _policies.Add(policy);
        return this;
    }

    public RequestPolicyBuilder RetryOnTimeout(int times, int waitInSec)
    {
        var policy = Policy.Handle<FlurlHttpTimeoutException>()
            .WaitAndRetryAsync(times, _ => TimeSpan.FromSeconds(waitInSec));

        _policies.Add(policy);
        return this;
    }

    public RequestPolicyBuilder FallbackOnNotFound()
    {
        var policy = Policy.Handle<TbHttpException>(x => x.StatusCode == HttpStatusCode.NotFound)
            .FallbackAsync(_ => Task.CompletedTask);

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

public class RequestPolicyBuilder<TResult>
{
    private readonly List<IAsyncPolicy<TResult>> _policies = new();

    public RequestPolicyBuilder<TResult> RetryOnUnauthorized()
    {
        var policy = Policy<TResult>.Handle<TbHttpException>(x => x.StatusCode == HttpStatusCode.Unauthorized)
            .RetryAsync(1);

        _policies.Add(policy);
        return this;
    }

    public RequestPolicyBuilder<TResult> RetryOnTimeout(int retryTimes, int retryWaitingInSec)
    {
        var policy = Policy<TResult>.Handle<FlurlHttpTimeoutException>()
            .WaitAndRetryAsync(retryTimes, _ => TimeSpan.FromSeconds(retryWaitingInSec));

        _policies.Add(policy);
        return this;
    }

    public RequestPolicyBuilder<TResult> FallbackOnNotFound(TResult fallbackValue)
    {
        var policy = Policy<TResult>.Handle<TbHttpException>(x => x.StatusCode == HttpStatusCode.NotFound)
            .FallbackAsync(fallbackValue);

        _policies.Add(policy);
        return this;
    }

    public IAsyncPolicy<TResult> Build()
    {
        return Policy.WrapAsync(_policies.ToArray());
    }
}
