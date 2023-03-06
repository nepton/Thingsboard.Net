namespace Thingsboard.Net;

public class TbKeyFilterDateTimePredicate : TbKeyFilterPredicate
{
    public override string                       Type      => "DATE_TIME";
    public          TbKeyFilterDateTimeOperation Operation { get; set; }
    public          TbKeyFilterValue             Value     { get; set; } = TbKeyFilterValue.Empty;

    public TbKeyFilterDateTimePredicate()
    {
    }

    public TbKeyFilterDateTimePredicate(TbKeyFilterDateTimeOperation operation, TbKeyFilterValue value)
    {
        Operation = operation;
        Value     = value;
    }
    
    public override string ToString()
    {
        return $"{Operation} {Value}";
    }
}
