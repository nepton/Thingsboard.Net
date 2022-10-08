namespace Thingsboard.Net.TbEntityQuery;

public class TbKeyFilterDynamicValue : TbKeyFilterValue
{
    public TbKeyFilterDynamicSourceType SourceType { get; }

    public string SourceAttribute { get; }

    public TbKeyFilterDynamicValue(TbKeyFilterDynamicSourceType sourceType, string sourceAttribute)
    {
        SourceType      = sourceType;
        SourceAttribute = sourceAttribute;
    }

    public override object ToQuery()
    {
        return new
        {
            dynamicValue = new
            {
                sourceType      = SourceType,
                sourceAttribute = SourceAttribute
            }
        };
    }
}
