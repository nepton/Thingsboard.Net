using System;

namespace Thingsboard.Net;

public class TbEntityQueryValue
{
    /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
    public TbEntityQueryValue(DateTime ts, object? value)
    {
        Ts    = ts;
        Value = value;
    }

    public DateTime Ts    { get; }
    public object?  Value { get; }
}
