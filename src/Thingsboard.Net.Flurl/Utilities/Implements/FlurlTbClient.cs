using System;
using Thingsboard.Net.Flurl.Options;

namespace Thingsboard.Net.Flurl.Utilities;

public abstract class FlurlTbClient<TClient> : ITbClient<TClient> where TClient : ITbClient<TClient>
{
    protected ThingsboardNetFlurlOptions? CustomOptions { get; private set; }

    public TClient WithCredentials(string username, string? password)
    {
        if (username == null) throw new ArgumentNullException(nameof(username));

        CustomOptions          ??= new();
        CustomOptions.Username =   username;
        CustomOptions.Password =   password;

        return (TClient) (object) this;
    }

    /// <summary>
    /// Use custom HTTP timeout
    /// </summary>
    /// <param name="timeoutInSec">HTTP timeout in milliseconds</param>
    /// <param name="retryTimes">When timeout occurred, retry times</param>
    /// <param name="retryIntervalInSec">Waiting time before retry in milliseconds</param>
    /// <returns></returns>
    public TClient WithHttpTimeout(int timeoutInSec, int retryTimes, int retryIntervalInSec)
    {
        if (timeoutInSec < 0) throw new ArgumentOutOfRangeException(nameof(timeoutInSec));
        if (retryTimes < 0) throw new ArgumentOutOfRangeException(nameof(retryTimes));
        if (retryIntervalInSec < 0) throw new ArgumentOutOfRangeException(nameof(retryIntervalInSec));

        CustomOptions                           ??= new();
        CustomOptions.TimeoutInSec              =   timeoutInSec;
        CustomOptions.RetryTimes         =   retryTimes;
        CustomOptions.RetryIntervalInSec =   retryIntervalInSec;

        return (TClient) (object) this;
    }

    /// <summary>
    /// Use custom baseUrl
    /// </summary>
    /// <param name="baseUrl">baseUrl of thingsboard server, MUST contains http:// or https://</param>
    /// <returns></returns>
    public TClient WithBaseUrl(string baseUrl)
    {
        if (baseUrl == null) throw new ArgumentNullException(nameof(baseUrl));

        CustomOptions         ??= new();
        CustomOptions.BaseUrl =   baseUrl;

        return (TClient) (object) this;
    }
}
