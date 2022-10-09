using System;
using Thingsboard.Net.Models;

namespace Thingsboard.Net.Options;

public sealed class ThingsboardNetOptions
{
    /// <summary>
    /// server address for example http://localhost:8080
    /// MUST contains protocol, server address and port
    /// </summary>
    public string? Url { get; set; }

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
    public ThingsboardNetOptions MergeWith(ThingsboardNetOptions? customOptions)
    {
        if (customOptions == null)
            return this;

        return new ThingsboardNetOptions
        {
            Username                 = customOptions.Username ?? Username,
            Password                 = customOptions.Password ?? Password,
            Url                      = customOptions.Url ?? Url,
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
