using System;

namespace Thingsboard.Net.TbDevices;

/// <summary>
/// 设备信息
/// </summary>
public class TbDevice
{
    /// <summary>
    /// 电池组的Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 设备的配置类型
    /// </summary>
    public Guid ProfileId { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 标签
    /// </summary>
    public string? Label { get; set; }
}
