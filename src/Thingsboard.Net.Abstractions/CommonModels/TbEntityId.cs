using System;

namespace Thingsboard.Net;

/// <summary>
/// Entity object Id
/// The object is equal with json object {entityType:entityType, id:id}
/// </summary>
public record TbEntityId
{
    public static readonly TbEntityId Empty = new(0, Guid.Empty);

    /// <summary>
    /// Entity object Id
    /// The object is equal with json object {entityType:entityType, id:id}
    /// </summary>
    public TbEntityId(TbEntityType entityType, Guid id)
    {
        Id         = id;
        EntityType = entityType;
    }

    /// <summary>
    /// data init with property
    /// </summary>
    public TbEntityId()
    {
    }

    /// <summary>
    /// The Id of the entity
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// The type of the entity
    /// </summary>
    public TbEntityType EntityType { get; init; }
}
