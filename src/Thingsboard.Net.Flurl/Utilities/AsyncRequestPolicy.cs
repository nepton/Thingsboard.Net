using System;
using System.Threading.Tasks;
using Polly;

namespace Thingsboard.Net.Flurl.Utilities;

public class AsyncRequestPolicy : IAsyncRequestPolicy
{
    private readonly IAsyncPolicy    _policy;
    private readonly IRequestBuilder _requestBuilder;

    public AsyncRequestPolicy(IRequestBuilder requestBuilder, IAsyncPolicy policy)
    {
        _policy         = policy;
        _requestBuilder = requestBuilder;
    }

    public Task ExecuteAsync(Func<IRequestBuilder, Task> action)
    {
        return _policy.ExecuteAsync(() => action(_requestBuilder));
    }
}
