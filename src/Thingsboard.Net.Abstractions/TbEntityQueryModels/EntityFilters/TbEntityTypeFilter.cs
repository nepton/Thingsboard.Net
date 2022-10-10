namespace Thingsboard.Net;

public class TbEntityTypeFilter : TbEntityFilter
{
    public override string Type => "entityType";

    public TbEntityType EntityType { get; }

    public TbEntityTypeFilter()
    {
    }

    public TbEntityTypeFilter(TbEntityType type)
    {
        EntityType = type;
    }
}
