using System;

namespace Thingsboard.Net;

/// <summary>
/// The class that represents the entity value.
/// </summary>
public class TbEntityTsValue
{
    /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
    public TbEntityTsValue(DateTime ts, object? value)
    {
        Ts    = ts;
        Value = value;
    }

    /// <summary>
    /// The timestamp of the value.
    /// </summary>
    public DateTime Ts { get; }

    /// <summary>
    /// The value.
    /// </summary>
    public object? Value { get; }

    /// <summary>Returns a string that represents the current object.</summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
        return $"{nameof(Ts)}: {Ts}, {nameof(Value)}: {Value}";
    }
}
