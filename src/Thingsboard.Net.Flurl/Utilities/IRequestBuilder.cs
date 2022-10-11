using Flurl.Http;
using Thingsboard.Net.Flurl.Options;

namespace Thingsboard.Net.Flurl.Utilities;

public interface IRequestBuilder
{
    /// <summary>
    /// Create a new request builder for the specified URL without any authentication
    /// </summary>
    /// <param name="customOptions"></param>
    /// <param name="requireAuthorization"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="TbException"></exception>
    /// <exception cref="TbHttpException"></exception>
    IFlurlRequest CreateRequest(ThingsboardNetFlurlOptions? customOptions, bool requireAuthorization = true);

    /// <summary>
    /// Create a new FlurlRequest with a retry policy
    /// </summary>
    /// <returns></returns>
    RequestPolicyBuilder GetPolicyBuilder(ThingsboardNetFlurlOptions? customOptions);

    /// <summary>
    /// Create a new FlurlRequest with a retry policy
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    RequestPolicyBuilder<TResult> GetPolicyBuilder<TResult>(ThingsboardNetFlurlOptions? customOptions);
}
