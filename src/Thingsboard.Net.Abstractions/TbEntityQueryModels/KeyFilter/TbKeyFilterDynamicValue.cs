namespace Thingsboard.Net;

public class TbKeyFilterDynamicValue : TbKeyFilterValue
{
    public TbKeyFilterDynamicSourceType SourceType { get; }

    public string SourceAttribute { get; }

    public TbKeyFilterDynamicValue(TbKeyFilterDynamicSourceType sourceType, string sourceAttribute)
    {
        SourceType      = sourceType;
        SourceAttribute = sourceAttribute;
    }
}
