using System;

namespace Thingsboard.Net.TbTimeSeries;

/// <summary>
/// 通过 /api/plugins/telemetry/{entityType}/{entityId}/values/timeSeries 获取的值的返回值模型
/// 默认 tb 的返回值是错误的，需要自己定义
/// </summary>
public class TbTimeSeriesValue
{
    public DateTime Time { get; set; }

    public object? Value { get; set; }
}
