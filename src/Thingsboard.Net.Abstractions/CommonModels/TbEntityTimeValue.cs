using System;

namespace Thingsboard.Net;

public class TbEntityTimeValue
{
    /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
    public TbEntityTimeValue(DateTime ts, object? value)
    {
        Ts    = ts;
        Value = value;
    }

    public DateTime Ts    { get; }
    public object?  Value { get; }
}
