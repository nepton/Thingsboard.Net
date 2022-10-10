namespace Thingsboard.Net;

public enum TbAttributeScope
{
    /// <summary>
    /// Value of the server property
    /// </summary>
    SERVER_SCOPE,

    /// <summary>
    /// Attribute value uploaded by the client
    /// </summary>
    CLIENT_SCOPE,

    /// <summary>
    /// Attribute value delivered by the server
    /// </summary>
    SHARED_SCOPE,
}
