using System.Threading.Tasks;
using Flurl.Http;
using Thingsboard.Net.Flurl.Options;

namespace Thingsboard.Net.Flurl.Utilities;

public interface IRequestBuilder
{
    /// <summary>
    /// Merge the options with the default options
    /// </summary>
    /// <param name="customOptions"></param>
    /// <returns></returns>
    IRequestBuilder MergeCustomOptions(ThingsboardNetFlurlOptions? customOptions);

    /// <summary>
    /// Create a new request builder for the specified URL without any authentication
    /// </summary>
    /// <returns></returns>
    IFlurlRequest CreateRequest();

    /// <summary>
    /// Create a new FlurlRequest with a retry policy
    /// </summary>
    /// <returns></returns>
    RequestPolicyBuilder GetPolicyBuilder();

    /// <summary>
    /// Create a new FlurlRequest with a retry policy
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    RequestPolicyBuilder<TResult> GetPolicyBuilder<TResult>();

    /// <summary>
    /// Get the access token with the user defined in the options
    /// </summary>
    /// <returns></returns>
    Task<string> GetAccessTokenAsync();
}
