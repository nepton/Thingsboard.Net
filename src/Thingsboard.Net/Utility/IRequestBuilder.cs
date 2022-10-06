using System;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Polly;
using Thingsboard.Net.Exceptions;

namespace Thingsboard.Net.Utility;

public interface IRequestBuilder
{
    Task<IFlurlRequest> CreateRequest(string path, CancellationToken cancel)
    {
        return CreateRequest(path, new TbClientApiOptions(), cancel);
    }

    Task<IFlurlRequest> CreateRequest(string path, TbClientApiOptions tbClientApiOptions, CancellationToken cancel);

    IAsyncPolicy CreatePolicy();

    /// <summary>
    /// Create a new request builder for the specified URL without any authentication
    /// </summary>
    /// <param name="path"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="TbException"></exception>
    /// <exception cref="TbHttpException"></exception>
    IFlurlRequest CreateAnonymousRequest(string path, TbClientApiOptions options);

    /// <summary>
    /// Create a new FlurlRequest with a retry policy
    /// </summary>
    /// <returns></returns>
    IAsyncPolicy CreateAnonymousPolicy();
}
