using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Polly;

namespace Thingsboard.Net.Utility;

public interface IRequestBuilder
{
    Task<IFlurlRequest> CreateRequest(string path, CancellationToken cancel)
    {
        return CreateRequest(path, new TbClientApiOptions(), cancel);
    }

    Task<IFlurlRequest> CreateRequest(string path, TbClientApiOptions tbClientApiOptions, CancellationToken cancel);

    IAsyncPolicy CreatePolicy();
}
