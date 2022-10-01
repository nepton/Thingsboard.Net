using System;

namespace Thingsboard.Net.TbAuth;

public class TbLoginRequest
{
    public string Username { get; }
    public string Password { get; }

    public TbLoginRequest(string username, string password)
    {
        Username = username ?? throw new ArgumentNullException(nameof(username));
        Password = password ?? throw new ArgumentNullException(nameof(password));
    }

    public object ToJsonObject()
    {
        return new
        {
            username = Username,
            password = Password
        };
    }
}
