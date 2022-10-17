namespace Thingsboard.Net;

/// <summary>
/// Entity field type.
/// </summary>
public enum TbEntityFieldType
{
    /// <summary>
    /// CLIENT_ATTRIBUTE 
    /// </summary>
    CLIENT_ATTRIBUTE,

    /// <summary>
    ///SHARED_ATTRIBUTE
    /// </summary>
    SHARED_ATTRIBUTE,

    /// <summary>
    ///SERVER_ATTRIBUTE
    /// </summary>
    SERVER_ATTRIBUTE,

    /// <summary>
    ///ATTRIBUTE
    /// </summary>
    ATTRIBUTE,

    /// <summary>
    ///TIME_SERIES
    /// </summary>
    TIME_SERIES,

    /// <summary>
    ///ENTITY_FIELD
    /// </summary>
    ENTITY_FIELD,

    /// <summary>
    ///ALARM_FIELD
    /// </summary>
    ALARM_FIELD,
}
