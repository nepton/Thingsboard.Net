namespace Thingsboard.Sdk.Models;

/// <summary>
/// 电池组设备的客户端属性
/// </summary>
public class DeviceServerAttributes
{
    /// <summary>
    /// 当前的 active 状态
    /// </summary>
    public static TbEntityField Active => new("active", TbEntityFieldType.SERVER_ATTRIBUTE);

    /// <summary>
    /// 最后的连接时间
    /// </summary>
    public static TbEntityField LastConnectTime => new("lastConnectTime", TbEntityFieldType.SERVER_ATTRIBUTE);

    /// <summary>
    /// 最后的断开时间
    /// </summary>
    public static TbEntityField LastDisconnectTime => new("lastDisconnectTime", TbEntityFieldType.SERVER_ATTRIBUTE);
}
