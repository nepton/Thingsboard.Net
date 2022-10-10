using System;

namespace Thingsboard.Net.Flurl.Options;

public sealed class ThingsboardNetFlurlOptions
{
    /// <summary>
    /// server address for example http://localhost:8080
    /// MUST contains protocol, server address and port
    /// </summary>
    public string? BaseUrl { get; set; }

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

    public int? TimeoutInSec { get; set; }

    /// <summary>
    /// Merge default options with custom options
    /// </summary>
    /// <param name="customOptions">return customOptions if available</param>
    /// <returns></returns>
    public ThingsboardNetFlurlOptions MergeWith(ThingsboardNetFlurlOptions? customOptions)
    {
        if (customOptions == null)
            return this;

        return new ThingsboardNetFlurlOptions
        {
            Username                 = customOptions.Username ?? Username,
            Password                 = customOptions.Password ?? Password,
            BaseUrl                      = customOptions.BaseUrl ?? BaseUrl,
            DynamicTokenExpiresInSec = customOptions.DynamicTokenExpiresInSec ?? DynamicTokenExpiresInSec,
            TimeoutInSec             = customOptions.TimeoutInSec ?? TimeoutInSec
        };
    }

    public TbCredentials GetCredentials()
    {
        return new TbCredentials(
            Username ?? throw new InvalidOperationException("Username does not set"),
            Password ?? ""
        );
    }
}
