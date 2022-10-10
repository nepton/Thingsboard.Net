namespace Thingsboard.Net;

/// <summary>
/// The class is used to store the information about the user.
/// </summary>
public class TbCredentials
{
    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public TbCredentials(string username, string password)
    {
        Username = username;
        Password = password;
    }

    /// <summary>
    /// The Username of the user.
    /// </summary>
    public string Username { get; }

    /// <summary>
    /// The password of the user.
    /// </summary>
    public string Password { get; }
}
