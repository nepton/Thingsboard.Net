using System;
using System.Threading.Tasks;

namespace Thingsboard.Net.Flurl.Utilities;

public interface IAsyncRequestPolicy
{
    Task ExecuteAsync(Func<IRequestBuilder, Task> action);
}
