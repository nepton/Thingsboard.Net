using System;

namespace Thingsboard.Sdk.Models;

/// <summary>
/// 实体对象Id
/// </summary>
public class TbEntityId
{
    public static TbEntityId Empty => new();

    private TbEntityId()
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public TbEntityId(Guid id, TbEntityType type)
    {
        Type = type;
        Id   = id;
    }

    public TbEntityType Type { get; }

    public Guid Id { get; }
}
