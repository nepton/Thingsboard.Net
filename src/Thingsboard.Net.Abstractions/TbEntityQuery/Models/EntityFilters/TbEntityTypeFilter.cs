using Thingsboard.Net.Models;

namespace Thingsboard.Net.TbEntityQuery;

public class TbEntityTypeFilter : TbEntityFilter
{
    public override string       Type =>
    public          TbEntityType Type { get; }

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
