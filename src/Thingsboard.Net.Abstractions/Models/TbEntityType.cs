namespace Thingsboard.Net.Models;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// Convert all properties to UPPER_STRING for EntityQuery
/// </remarks>
public enum TbEntityType
{
    ALARM           = 1,
    API_USAGE_STATE = 2,
    ASSET           = 3,
    CUSTOMER        = 4,
    DASHBOARD       = 5,
    DEVICE          = 6,
    DEVICE_PROFILE  = 7,
    EDGE            = 8,
    ENTITY_VIEW     = 9,
    OTA_PACKAGE     = 10,
    RPC             = 11,
    RULE_CHAIN      = 12,
    RULE_NODE       = 13,
    TB_RESOURCE     = 14,
    TENANT          = 15,
    TENANT_PROFILE  = 16,
    USER            = 17,
    WIDGETS_BUNDLE  = 18,
    WIDGET_TYPE     = 19
}
