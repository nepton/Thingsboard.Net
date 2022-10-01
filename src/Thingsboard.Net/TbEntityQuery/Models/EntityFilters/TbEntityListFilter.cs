using System;
using Thingsboard.Net.Models;

namespace Thingsboard.Net.TbEntityQuery;

public class TbEntityListFilter : TbEntityFilter
{
    public TbEntityListFilter(TbEntityType type, Guid[] entityIds)
    {
        EntityIds = entityIds;
        Type      = type;
    }

    public Guid[] EntityIds { get; }

    public TbEntityType Type { get; }

    public override object ToQuery()
    {
        return new
        {
            type       = "entityList",
            entityType = Type,
            entityList = EntityIds
        };
    }
}
