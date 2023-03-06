namespace Thingsboard.Net;

public class TbKeyFilterBooleanValuePredicate : TbKeyFilterPredicate
{
    public override string Type => "BOOLEAN";

    public TbKeyFilterBooleanOperation Operation { get; set; }

    /// <summary>
    /// The value to be used for filtering.
    /// </summary>
    public TbKeyFilterValue Value { get; set; } = TbKeyFilterValue.Empty;

    public TbKeyFilterBooleanValuePredicate()
    {
    }

    public TbKeyFilterBooleanValuePredicate(TbKeyFilterBooleanOperation operation, TbKeyFilterValue value)
    {
        Operation = operation;
        Value     = value;
    }
}
