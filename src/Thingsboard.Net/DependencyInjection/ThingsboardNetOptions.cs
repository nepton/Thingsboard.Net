using System;

namespace Thingsboard.Net.DependencyInjection;

public sealed class ThingsboardNetOptions
{
    /// <summary>
    /// server address for example http://localhost:8080
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// 获取 URL
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public string GetUrl()
    {
        if (string.IsNullOrEmpty(Url))
            throw new ArgumentNullException(nameof(Url), "Thingsboard URL is not set");

        return Url;
    }

    /// <summary>
    /// The access token for the Thingsboard
    /// The access token will be used to authenticate the client on the Thingsboard server priority then username and password
    /// </summary>
    public string? AccessToken { get; set; }

    /// <summary>
    /// the user name to acquire dynamic token to access thingsboard
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// the password to acquire dynamic token to to access thingsboard
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// The validity period of the default token is not provided to us by the TB system, so we need to solve this problem by ourselves
    /// </summary>
    public int? DynamicTokenExpiresInSec { get; set; }

    public int GetDynamicTokenExpiresInSec()
    {
        if (DynamicTokenExpiresInSec == null)
            return 3600;
        return DynamicTokenExpiresInSec.Value;
    }

    public int? TimeoutInSec { get; set; }

    /// <summary>
    /// Get the timeout in seconds
    /// </summary>
    /// <returns></returns>
    public int GetTimeoutInSecOrDefault()
    {
        return TimeoutInSec ?? 10;
    }
}
