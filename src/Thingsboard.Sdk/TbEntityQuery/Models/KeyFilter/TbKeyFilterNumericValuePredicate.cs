namespace Thingsboard.Sdk.TbEntityQuery;

public class TbKeyFilterNumericValuePredicate : TbKeyFilterPredicate
{
    public TbKeyFilterNumericOperation Operation { get; }

    /// <summary>
    /// The value to be used for filtering.
    /// </summary>
    public TbKeyFilterValue Value { get; }

    public TbKeyFilterNumericValuePredicate(TbKeyFilterNumericOperation operation, TbKeyFilterValue value)
    {
        Operation = operation;
        Value     = value;
    }

    public override object ToQuery()
    {
        return new
        {
            operation = Operation,
            value     = Value.ToQuery(),
            type      = "NUMERIC",
        };
    }
}
