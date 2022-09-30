using System;

namespace Thingsboard.Sdk.TbTimeSeries;

/// <summary>
/// 时序记录项
/// </summary>
public class TbTimeSeries
{
    public string Key { get; set; } = "";

    public TbTimeSeriesValue[] Values { get; set; } = Array.Empty<TbTimeSeriesValue>();
}
