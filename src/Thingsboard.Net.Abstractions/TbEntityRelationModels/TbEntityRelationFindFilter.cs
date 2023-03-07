namespace Thingsboard.Net.TbEntityRelationModels;

/// <summary>
/// The entity relation query filter.
/// </summary>
public class TbEntityRelationFindFilter
{
    /// <summary>
    /// The relation type
    /// </summary>
    public string? RelationType { get; set; }

    /// <summary>
    /// The entity types
    /// </summary>
    public TbEntityType[]? EntityTypes { get; set; }
}