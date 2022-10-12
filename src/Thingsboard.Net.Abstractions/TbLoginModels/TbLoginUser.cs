using System;

namespace Thingsboard.Net;

public class TbLoginUser
{
    public string Username { get; }
    public string Password { get; }

    public TbLoginUser(string username, string password)
    {
        Username = username ?? throw new ArgumentNullException(nameof(username));
        Password = password ?? throw new ArgumentNullException(nameof(password));
    }

    public TbLoginUser(TbCredentials credentials)
    {
        if (credentials == null) throw new ArgumentNullException(nameof(credentials));

        Username = credentials.Username ?? throw new ArgumentNullException(nameof(credentials.Username));
        Password = credentials.Password ?? throw new ArgumentNullException(nameof(credentials.Password));
    }
}
