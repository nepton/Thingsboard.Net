namespace Thingsboard.Net.TbEntityQuery;

/// <summary>
/// Note that you may use 'CURRENT_USER', 'CURRENT_CUSTOMER' and 'CURRENT_TENANT' as a 'sourceType'. The 'defaultValue' is used when the attribute with such a name is not defined for the chosen source.
/// </summary>
public enum TbKeyFilterDynamicSourceType
{
    CURRENT_USER, CURRENT_CUSTOMER, CURRENT_TENANT,
}