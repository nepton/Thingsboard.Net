namespace Thingsboard.Net.TbEntityQuery;

public class TbKeyFilterDateTimePredicate : TbKeyFilterPredicate
{
    public TbKeyFilterDateTimeOperation Operation { get; }
    public TbKeyFilterValue             Value     { get; }

    public TbKeyFilterDateTimePredicate(TbKeyFilterDateTimeOperation operation, TbKeyFilterValue value)
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
            type      = "DATE_TIME",
        };
    }
}
