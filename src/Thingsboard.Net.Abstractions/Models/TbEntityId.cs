using System;

namespace Thingsboard.Net.Models;

/// <summary>
/// Entity object Id
/// The object is equal with json object {entityType:entityType, id:id}
/// </summary>
public class TbEntityId
{
    public static readonly TbEntityId Empty = new(0, Guid.Empty);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Object" /> class.
    /// </summary>
    /// <remarks>
    /// see TbUserInfo
    /// </remarks>
    public TbEntityId(TbEntityType entityType, Guid id)
    {
        EntityType = entityType;
        Id         = id;
    }

    public Guid Id { get; }

    public TbEntityType EntityType { get; }

    /// <summary>Returns a string that represents the current object.</summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(EntityType)}: {EntityType}";
    }
}
