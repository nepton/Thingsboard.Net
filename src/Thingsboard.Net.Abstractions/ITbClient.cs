namespace Thingsboard.Net;

/// <summary>
/// The base interface for all Thingsboard client.
/// </summary>
/// <typeparam name="TClient"></typeparam>
public interface ITbClient<out TClient> where TClient : ITbClient<TClient>
{
    /// <summary>
    /// Use custom credentials
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    TClient WithCredentials(string username, string? password);

    /// <summary>
    /// Use custom HTTP timeout
    /// </summary>
    /// <param name="timeoutInSec">HTTP timeout in seconds</param>
    /// <param name="retryTimes">When timeout occurred, retry times</param>
    /// <param name="retryIntervalInSec">Waiting time before retry in seconds</param>
    /// <returns></returns>
    TClient WithHttpTimeout(int timeoutInSec, int retryTimes, int retryIntervalInSec);

    /// <summary>
    /// Use custom baseUrl
    /// </summary>
    /// <param name="baseUrl">baseUrl of thingsboard server, MUST contains http:// or https://</param>
    /// <returns></returns>
    TClient WithBaseUrl(string baseUrl);
}
