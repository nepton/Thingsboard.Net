namespace Thingsboard.Net;

public class TbKeyFilterStringValuePredicate : TbKeyFilterPredicate
{
    public override string Type => "STRING";

    public TbKeyFilterStringOperation Operation { get; set; }

    /// <summary>
    /// The value to be used for filtering.
    /// </summary>
    public TbKeyFilterValue Value { get; set; } = TbKeyFilterValue.Empty;

    public TbKeyFilterStringValuePredicate()
    {
    }

    public TbKeyFilterStringValuePredicate(TbKeyFilterStringOperation operation, TbKeyFilterValue value)
    {
        Operation = operation;
        Value     = value;
    }

    public override string ToString()
    {
        return $"{Operation} {Value}";
    }
}
