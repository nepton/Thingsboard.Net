using System;

namespace Thingsboard.Net.Flurl.Utility.Models;

public class AccessTokenModel
{
    public string? Username { get; set; }

    public string? Password { get; set; }

    public string AccessToken { get; set; } = "";

    public DateTime ExpiresAt { get; set; }
}
