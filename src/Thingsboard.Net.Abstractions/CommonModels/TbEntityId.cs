using System;

namespace Thingsboard.Net;

/// <summary>
/// Entity object Id
/// The object is equal with json object {entityType:entityType, id:id}
/// </summary>
public record TbEntityId(TbEntityType EntityType, Guid Id)
{
    public static readonly TbEntityId Empty = new(0, Guid.Empty);

    /// <summary>
    /// The Id of the entity
    /// </summary>
    public Guid Id { get; } = Id;

    /// <summary>
    /// The type of the entity
    /// </summary>
    public TbEntityType EntityType { get; } = EntityType;
}
