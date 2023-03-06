namespace Thingsboard.Net;

public class TbKeyFilterValue
{
    public static readonly TbKeyFilterValue Empty = new();

    public object? DefaultValue { get; set; }

    public TbKeyFilterDynamicValue? DynamicValue { get; set; }
}
