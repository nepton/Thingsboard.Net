namespace Thingsboard.Net.TbEntityQuery;

public class TbKeyFilterBooleanValuePredicate : TbKeyFilterPredicate
{
    public TbKeyFilterBooleanOperation Operation { get; }

    /// <summary>
    /// The value to be used for filtering.
    /// </summary>
    public TbKeyFilterValue Value { get; }

    public TbKeyFilterBooleanValuePredicate(TbKeyFilterBooleanOperation operation, TbKeyFilterValue value)
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
            type      = "BOOLEAN",
        };
    }
}
