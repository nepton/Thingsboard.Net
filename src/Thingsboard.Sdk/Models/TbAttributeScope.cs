namespace Thingsboard.Sdk.Models;

public enum TbAttributeScope
{
    /// <summary>
    /// 服务端属性值
    /// </summary>
    Server,

    /// <summary>
    /// 客户端上传的属性值
    /// </summary>
    Client,

    /// <summary>
    /// 服务器下发的属性值
    /// </summary>
    Shared,
}
