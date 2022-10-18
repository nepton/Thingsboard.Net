using System;

namespace Thingsboard.Net;

/// <summary>
/// The returned entity attribute value
/// </summary>
public class TbEntityKeyValue
{
    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public TbEntityKeyValue(string key, DateTime lastUpdateTs, object? value)
    {
        Key          = key;
        LastUpdateTs = lastUpdateTs;
        Value        = value;
    }

    public string   Key          { get; }
    public DateTime LastUpdateTs { get; }

    public object? Value { get; }

    /// <summary>Returns a string that represents the current object.</summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
        return $"{nameof(Key)}: {Key}, {nameof(LastUpdateTs)}: {LastUpdateTs}, {nameof(Value)}: {Value}";
    }
}
