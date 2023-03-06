namespace Thingsboard.Net;

public class TbKeyFilterNumericValuePredicate : TbKeyFilterPredicate
{
    public override string Type => "NUMERIC";

    public TbKeyFilterNumericOperation Operation { get; set; }

    /// <summary>
    /// The value to be used for filtering.
    /// </summary>
    public TbKeyFilterValue Value { get; set; } = TbKeyFilterValue.Empty;

    public TbKeyFilterNumericValuePredicate()
    {
    }

    public TbKeyFilterNumericValuePredicate(TbKeyFilterNumericOperation operation, TbKeyFilterValue value)
    {
        Operation = operation;
        Value     = value;
    }

    public override string ToString()
    {
        return $"{Operation} {Value}";
    }
}
