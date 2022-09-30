namespace Thingsboard.Sdk.TbEntityQuery;

public class TbKeyFilterConstantValue<T> : TbKeyFilterValue
{
    public T Value { get; }

    public TbKeyFilterConstantValue(T value)
    {
        Value = value;
    }

    public override object ToQuery()
    {
        return new
        {
            defaultValue = Value,
        };
    }
}
