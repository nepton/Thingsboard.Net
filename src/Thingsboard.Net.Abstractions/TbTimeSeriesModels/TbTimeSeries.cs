using System;

namespace Thingsboard.Net;

/// <summary>
/// 时序记录项
/// </summary>
public class TbTimeSeries
{
    public string Key { get; set; } = "";

    public TbTimeSeriesValue[] Values { get; set; } = Array.Empty<TbTimeSeriesValue>();
}
