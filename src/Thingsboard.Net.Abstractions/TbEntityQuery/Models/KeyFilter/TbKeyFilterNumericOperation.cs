namespace Thingsboard.Net.TbEntityQuery;

/// <summary>
/// 'NUMERIC' - used for 'Long' and 'Double' values. Operations: EQUAL, NOT_EQUAL, GREATER, LESS, GREATER_OR_EQUAL, LESS_OR_EQUAL;
/// </summary>
public enum TbKeyFilterNumericOperation
{
    EQUAL,
    NOT_EQUAL,
    GREATER,
    LESS,
    GREATER_OR_EQUAL,
    LESS_OR_EQUAL
}