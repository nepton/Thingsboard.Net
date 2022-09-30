using System;
using System.Collections.Generic;
using Thingsboard.Sdk.Models;

namespace Thingsboard.Sdk.TbAlarms;

/// <summary>
/// TB 系统的报警信息对象
/// </summary>
public class TbAlarm
{
    /// <summary>
    /// 警报Id
    /// </summary>
    public Guid TbAlarmId { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    public string Type { get; set; } = "";

    /// <summary>
    /// 发送者
    /// </summary>
    public TbEntityId Originator { get; set; } = TbEntityId.Empty;

    /// <summary>
    /// 严重程度
    /// </summary>
    public TbAlarmSeverity Severity { get; set; }

    /// <summary>
    /// 已经被清除
    /// </summary>
    public bool Cleared { get; set; }

    /// <summary>
    /// 已被确认
    /// </summary>
    public bool Acknowledged { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 结束时间 (这个时间是最后一次收到报警信息的时间)
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// 发送确认命令的时间
    /// </summary>
    public DateTime AckTime { get; set; }

    /// <summary>
    /// 发送清除命令的时间
    /// </summary>
    public DateTime ClearTime { get; set; }

    /// <summary>
    /// 警报信息的详情
    /// </summary>
    public Dictionary<string, object> Details { get; set; } = new();
}
