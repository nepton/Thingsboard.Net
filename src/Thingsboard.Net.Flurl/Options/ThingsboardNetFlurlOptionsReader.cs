using System;
using System.Collections.Generic;
using System.Linq;
using Flurl;
using Thingsboard.Net.Exceptions;
using Thingsboard.Net.Flurl.Utilities;

namespace Thingsboard.Net.Flurl.Options;

/// <summary>
/// This class is used to merge the multi options
/// </summary>
public sealed class ThingsboardNetFlurlOptionsReader
{
    private readonly List<ThingsboardNetFlurlOptions> _optionsList;

    public ThingsboardNetFlurlOptions[] Options => _optionsList.ToArray();

    public static readonly ThingsboardNetFlurlOptions DefaultOptions = new()
    {
        BaseUrl            = "http://localhost:8080",
        Username           = "tenant@thingsboaard.org",
        Password           = "tenant",
        TimeoutInSec       = 10,
        RetryTimes         = 3,
        RetryIntervalInSec = 1,
    };

    /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
    public ThingsboardNetFlurlOptionsReader(params ThingsboardNetFlurlOptions[] options)
    {
        _optionsList = options.ToList();
    }

    /// <summary>
    /// add more options
    /// </summary>
    /// <param name="options"></param>
    public void AddHighPriorityOptions(ThingsboardNetFlurlOptions options)
    {
        if (options == null) throw new ArgumentNullException(nameof(options));

        _optionsList.Insert(0, options);
    }

    /// <summary>
    /// The policy of the options reader
    /// </summary>
    /// <param name="func"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private T GetValue<T>(Func<ThingsboardNetFlurlOptions, T> func)
    {
        foreach (var options in _optionsList)
        {
            var value = func(options);
            if (value != null)
            {
                return value;
            }
        }

        return func(DefaultOptions);
    }

    /// <summary>
    /// server address for example http://localhost:8080
    /// MUST contains protocol, server address and port
    /// </summary>
    public Url BaseUrl
    {
        get
        {
            var value = GetValue(x => x.BaseUrl!);

            if (string.IsNullOrEmpty(value))
                throw new TbOptionsException(nameof(BaseUrl), "BaseUrl is empty or null");

            var url = new Url(value);
            if (url.IsRelative)
                throw new TbOptionsException(nameof(BaseUrl), $"BaseUrl must be absolute url: {value}");

            if (url.Scheme != "http" && url.Scheme != "https")
                throw new TbOptionsException(nameof(BaseUrl), $"BaseUrl must be http or https: {value}");

            return value;
        }
    }

    public TbCredentials Credentials => new(GetValue(x => x.Username).MakeSureNotNull(), GetValue(x => x.Password));

    public int TimeoutInSec => GetValue(x => x.TimeoutInSec) ?? throw new TbOptionsException(nameof(TimeoutInSec), "TimeoutInSec is null");

    /// <summary>
    /// When HTTP timeout occurs, the number of retries
    /// </summary>
    public int RetryTimes => GetValue(x => x.RetryTimes) ?? throw new TbOptionsException(nameof(RetryTimes), "RetryTimes is null");

    /// <summary>
    /// When HTTP timeout occurs, the interval between retries
    /// </summary>
    public int RetryIntervalInSec => GetValue(x => x.RetryIntervalInSec) ?? throw new TbOptionsException(nameof(RetryIntervalInSec), "RetryIntervalInSec is null");
}
