using System;

namespace Thingsboard.Net;

/// <summary>
/// The default tb return value is incorrect and needs to be defined yourself
/// </summary>
public class TbTimeSeriesValue
{
    public DateTime Ts { get; set; }

    public object? Value { get; set; }
}
