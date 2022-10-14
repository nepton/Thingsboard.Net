using System;
using System.Threading.Tasks;
using Polly;

namespace Thingsboard.Net.Flurl.Utilities;

public class AsyncRequestPolicy<TResult> : IAsyncRequestPolicy<TResult>
{
    private readonly IAsyncPolicy<TResult> _policy;
    private readonly IRequestBuilder       _requestBuilder;

    public AsyncRequestPolicy(IRequestBuilder requestBuilder, IAsyncPolicy<TResult> policy)
    {
        _policy         = policy;
        _requestBuilder = requestBuilder;
    }

    public Task<TResult> ExecuteAsync(Func<IRequestBuilder, Task<TResult>> action)
    {
        return _policy.ExecuteAsync(() => action(_requestBuilder));
    }
}
