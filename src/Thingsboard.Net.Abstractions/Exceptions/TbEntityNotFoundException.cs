using System;

namespace Thingsboard.Net.Exceptions;

/// <summary>
/// 指定的实体在 TB 系统中无法找到
/// </summary>
public class TbEntityNotFoundException : Exception
{
    public TbEntityId EntityId { get; }

    public TbEntityNotFoundException(TbEntityType entityType, Guid entityId)
    {
        EntityId = new(entityType, entityId);
    }

    public TbEntityNotFoundException(TbEntityId entityId)
    {
        EntityId = entityId;
    }
}
