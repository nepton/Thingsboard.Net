namespace Thingsboard.Net.Exceptions;

/// <summary>
/// When the server returns a 401 Unauthorized response, the client should retry the request with a new access token.
/// </summary>
public class TbAuthorizationException : TbException
{
    public TbAuthorizationException(string message) : base(message)
    {
    }
}
