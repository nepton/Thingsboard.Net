using System;
using Thingsboard.Net.Models;

namespace Thingsboard.Net.TbEntityQuery;

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
