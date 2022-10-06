using System;
using Newtonsoft.Json;

namespace Thingsboard.Net.TbLogin;

public class TbLoginRequest
{
    [JsonProperty("username")] public string Username { get; }
    [JsonProperty("password")] public string Password { get; }

    [JsonConstructor]
    public TbLoginRequest(string username, string password)
    {
        Username = username ?? throw new ArgumentNullException(nameof(username));
        Password = password ?? throw new ArgumentNullException(nameof(password));
    }
}
