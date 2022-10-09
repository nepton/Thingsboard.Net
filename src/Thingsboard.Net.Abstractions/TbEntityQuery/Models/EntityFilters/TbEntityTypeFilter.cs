using Thingsboard.Net.Models;

namespace Thingsboard.Net.TbEntityQuery;

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
