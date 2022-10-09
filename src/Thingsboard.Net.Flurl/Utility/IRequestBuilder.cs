using System;
using Flurl.Http;
using Polly;
using Thingsboard.Net.Exceptions;
using Thingsboard.Net.Options;

namespace Thingsboard.Net.Flurl.Utility;

public interface IRequestBuilder
{
    /// <summary>
    /// Create a new request builder for the specified URL without any authentication
    /// </summary>
    /// <param name="path"></param>
    /// <param name="customOptions"></param>
    /// <param name="requireAuthorization"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="TbException"></exception>
    /// <exception cref="TbHttpException"></exception>
    IFlurlRequest CreateRequest(string path, ThingsboardNetOptions customOptions, bool requireAuthorization = true);

    /// <summary>
    /// Create a new FlurlRequest with a retry policy
    /// </summary>
    /// <returns></returns>
    RequestPolicyBuilder GetDefaultPolicy();

    /// <summary>
    /// Create a new FlurlRequest with a retry policy
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    RequestPolicyBuilder<TResult> GetDefaultPolicy<TResult>();
}
