namespace Thingsboard.Net;

public class TbKeyFilterStringValuePredicate : TbKeyFilterPredicate
{
    public override string Type => "STRING";

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
}
