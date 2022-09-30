using System;
using Thingsboard.Sdk.Models;

namespace Thingsboard.Sdk.TbEntityQuery;

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
