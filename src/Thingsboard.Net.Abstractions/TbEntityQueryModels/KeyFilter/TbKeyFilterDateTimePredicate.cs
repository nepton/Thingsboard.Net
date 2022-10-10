namespace Thingsboard.Net;

public class TbKeyFilterDateTimePredicate : TbKeyFilterPredicate
{
    public override string                       Type      => "DATE_TIME";
    public          TbKeyFilterDateTimeOperation Operation { get; }
    public          TbKeyFilterValue             Value     { get; }

    public TbKeyFilterDateTimePredicate(TbKeyFilterDateTimeOperation operation, TbKeyFilterValue value)
    {
        Operation = operation;
        Value     = value;
    }
}
