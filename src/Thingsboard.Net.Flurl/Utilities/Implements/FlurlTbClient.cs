using System;
using Flurl;
using Thingsboard.Net.Flurl.Options;

namespace Thingsboard.Net.Flurl.Utilities;

public abstract class FlurlTbClient<TClient> : ITbClient<TClient>, IUnitTestOptionsReader where TClient : ITbClient<TClient>
{
    private readonly IRequestBuilder _requestBuilder;

    private ThingsboardNetFlurlOptions? _customOptions;

    protected FlurlTbClient(IRequestBuilder requestBuilder)
    {
        _requestBuilder = requestBuilder;
    }

    protected IRequestBuilder RequestBuilder => _customOptions != null ? _requestBuilder.MergeCustomOptions(_customOptions) : _requestBuilder;

    /// <summary>
    /// This method is used to do unit test
    /// </summary>
    /// <returns></returns>
    ThingsboardNetFlurlOptions? IUnitTestOptionsReader.GetOptions() => _customOptions;

    public TClient WithCredentials(string username, string? password)
    {
        if (username == null) throw new ArgumentNullException(nameof(username));

        _customOptions          ??= new();
        _customOptions.Username =   username;
        _customOptions.Password =   password;

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

        _customOptions                    ??= new();
        _customOptions.TimeoutInSec       =   timeoutInSec;
        _customOptions.RetryTimes         =   retryTimes;
        _customOptions.RetryIntervalInSec =   retryIntervalInSec;

        return (TClient) (object) this;
    }

    /// <summary>
    /// Use custom baseUrl
    /// </summary>
    /// <param name="baseUrl">baseUrl of thingsboard server, MUST contains http:// or https://</param>
    /// <returns></returns>
    public TClient WithBaseUrl(string baseUrl)
    {
        if (string.IsNullOrEmpty(baseUrl))
            throw new ArgumentNullException(nameof(baseUrl), "Thingsboard URL is not set");

        var url = new Url(baseUrl);

        if (url.Scheme != "http" && url.Scheme != "https")
            throw new ArgumentException("Thingsboard URL must be http or https", nameof(Url));

        if (url.Host == null)
            throw new ArgumentException("Thingsboard URL must be contains host", nameof(Url));

        _customOptions         ??= new();
        _customOptions.BaseUrl =   baseUrl;

        return (TClient) (object) this;
    }
}
