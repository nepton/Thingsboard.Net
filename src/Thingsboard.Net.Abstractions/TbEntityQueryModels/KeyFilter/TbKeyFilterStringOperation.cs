namespace Thingsboard.Net;

/// <summary>
/// 'STRING' - used to filter any 'String' or 'JSON' values. Operations: EQUAL, NOT_EQUAL, STARTS_WITH, ENDS_WITH, CONTAINS, NOT_CONTAINS;
/// </summary>
public enum TbKeyFilterStringOperation
{
    EQUAL,
    NOT_EQUAL,
    STARTS_WITH,
    ENDS_WITH,
    CONTAINS,
    NOT_CONTAINS,
}
