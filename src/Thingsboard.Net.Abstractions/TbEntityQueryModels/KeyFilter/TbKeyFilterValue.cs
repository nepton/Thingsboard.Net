namespace Thingsboard.Net;

public class TbKeyFilterValue
{
    public static readonly TbKeyFilterValue Empty = new();

    public object? DefaultValue { get; set; }

    public TbKeyFilterDynamicValue? DynamicValue { get; set; }

    /// <summary>Returns a string that represents the current object.</summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
        var result = string.Empty;
        if (DefaultValue != null)
        {
            result += $"{DefaultValue}";
        }

        if (DynamicValue != null)
        {
            result += $"{DynamicValue}";
        }

        if (string.IsNullOrEmpty(result))
        {
            result = "<Empty>";
        }

        return result;
    }
}
