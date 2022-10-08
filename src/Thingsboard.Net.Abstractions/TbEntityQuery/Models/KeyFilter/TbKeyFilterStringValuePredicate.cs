namespace Thingsboard.Net.TbEntityQuery;

public class TbKeyFilterStringValuePredicate : TbKeyFilterPredicate
{
    public TbKeyFilterStringOperation Operation { get; }

    /// <summary>
    /// The value to be used for filtering.
    /// </summary>
    public TbKeyFilterValue Value { get; }

    public TbKeyFilterStringValuePredicate(TbKeyFilterStringOperation operation, TbKeyFilterValue value)
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
            type      = "STRING",
        };
    }
}
