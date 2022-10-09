namespace Thingsboard.Net.TbEntityQuery;

public class TbKeyFilterNumericValuePredicate : TbKeyFilterPredicate
{
    public override string Type => "NUMERIC";

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
}
