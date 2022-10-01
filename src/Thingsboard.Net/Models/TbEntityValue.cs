using System;

namespace Thingsboard.Net.Models;

/// <summary>
/// 返回的实体属性值
/// </summary>
public class TbEntityValue
{
    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public TbEntityValue(TbEntityField key, DateTime time, object? value)
    {
        Key   = key;
        Time  = time;
        Value = value;
    }

    public TbEntityField Key { get; }

    public DateTime Time { get; }

    public object? Value { get; }

    /// <summary>Returns a string that represents the current object.</summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
        return $"{nameof(Key)}: {Key}, {nameof(Time)}: {Time}, {nameof(Value)}: {Value}";
    }
}
