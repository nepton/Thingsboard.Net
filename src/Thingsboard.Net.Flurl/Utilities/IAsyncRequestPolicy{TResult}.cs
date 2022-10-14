using System;
using System.Threading.Tasks;

namespace Thingsboard.Net.Flurl.Utilities;

public interface IAsyncRequestPolicy<TResult>
{
    Task<TResult> ExecuteAsync(Func<IRequestBuilder, Task<TResult>> action);
}
