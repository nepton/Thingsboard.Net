namespace Thingsboard.Sdk.DependencyInjection;

public sealed class ThingsboardSdkOptions
{
    /// <summary>
    /// server address for example http://localhost:8080
    /// </summary>
    public string? Url { get; set; }

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
    public int? DynamicTokenExpiresInSec { get; set; } = 9000;

    public int TimeoutInSec { get; set; }
}
