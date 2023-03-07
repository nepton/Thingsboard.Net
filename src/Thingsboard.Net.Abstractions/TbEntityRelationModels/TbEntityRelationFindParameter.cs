using System;

namespace Thingsboard.Net.TbEntityRelationModels;

/// <summary>
/// The entity relation query parameter.
/// </summary>
public class TbEntityRelationFindParameter
{
    /// <summary>
    /// Root entity id.
    /// </summary>
    public Guid RootId { get; set; }

    /// <summary>
    /// Root entity type.
    /// </summary>
    public TbEntityType RootType { get; set; }

    /// <summary>
    /// The relation direction.
    /// </summary>
    public TbRelationDirection Direction { get; set; }

    /// <summary>
    /// The relation type group.
    /// </summary>
    public string? RelationTypeGroup { get; set; }

    /// <summary>
    /// Query max level.
    /// </summary>
    public int MaxLevel { get; set; }

    /// <summary>
    /// If true, only last level of relations will be fetched.
    /// </summary>
    public bool FetchLastLevelOnly { get; set; }
}