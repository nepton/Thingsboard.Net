namespace Thingsboard.Net;

public class TbLoginResponse
{
    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public TbLoginResponse(string? token, string? refreshToken)
    {
        Token        = token;
        RefreshToken = refreshToken;
    }

    public string? Token { get; }

    public string? RefreshToken { get; }
}
