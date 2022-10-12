using System;

namespace Thingsboard.Net.Flurl.Utilities.Models;

public class AccessTokenModel
{
    public string? Username { get; set; }
    public string? Password { get; set; }

    public string AccessToken  { get; set; } = "";
    public string RefreshToken { get; set; } = "";

    public DateTime  ExpiresAt  { get; set; }
    public Guid?     UserId     { get; set; }
    public string[]? Scopes     { get; set; }
    public DateTime  IssuedAt   { get; set; }
    public string?   Issuer     { get; set; }
    public bool?     IsPublic   { get; set; }
    public bool?     Enabled    { get; set; }
    public Guid?     TenantId   { get; set; }
    public Guid?     CustomerId { get; set; }
}
