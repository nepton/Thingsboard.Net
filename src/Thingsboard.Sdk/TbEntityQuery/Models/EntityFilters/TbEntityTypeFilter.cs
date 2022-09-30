using Thingsboard.Sdk.Models;

namespace Thingsboard.Sdk.TbEntityQuery;

public class TbEntityTypeFilter : TbEntityFilter
{
    public TbEntityType Type { get; }

    public TbEntityTypeFilter(TbEntityType type)
    {
        Type = type;
    }

    public override object ToQuery()
    {
        return new
        {
            type       = "entityType",
            entityType = Type,
        };
    }
}
