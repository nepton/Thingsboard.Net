using System;

namespace Thingsboard.Net.Models;

/// <summary>
/// Entity object Id
/// The object is equal with json object {entityType:entityType, id:id}
/// </summary>
public class TbEntityId
{
    public static TbEntityId Empty => new();

    private TbEntityId()
    {
    }

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

    public TbEntityType EntityType { get; }

    public Guid Id { get; }
}
