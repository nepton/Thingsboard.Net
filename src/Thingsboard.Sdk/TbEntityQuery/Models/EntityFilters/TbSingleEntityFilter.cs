using System;
using Thingsboard.Sdk.Models;

namespace Thingsboard.Sdk.TbEntityQuery;

/// <summary>
/// Single entity filter
/// </summary>
public class TbSingleEntityFilter : TbEntityFilter
{
    public TbEntityType EntityType { get; }

    public Guid EntityId { get; }

    public TbSingleEntityFilter(TbEntityType entityType, Guid entityId)
    {
        EntityType = entityType;
        EntityId   = entityId;
    }

    public override object ToQuery()
    {
        return new
        {
            type = "singleEntity",
            singleEntity = new
            {
                id         = EntityId,
                entityType = EntityType,
            }
        };
    }
}
