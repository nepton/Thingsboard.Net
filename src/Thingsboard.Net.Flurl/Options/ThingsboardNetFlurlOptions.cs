namespace Thingsboard.Net.Flurl.Options;

public sealed record ThingsboardNetFlurlOptions
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

    public int? TimeoutInSec { get; set; }

    /// <summary>
    /// When HTTP timeout occurs, the number of retries
    /// </summary>
    public int? RetryTimes { get; set; }

    /// <summary>
    /// When HTTP timeout occurs, the interval between retries
    /// </summary>
    public int? RetryIntervalInSec { get; set; }
}
