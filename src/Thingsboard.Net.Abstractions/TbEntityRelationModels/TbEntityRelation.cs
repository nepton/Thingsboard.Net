using System.Runtime.Serialization;

namespace Thingsboard.Net.TbEntityRelationModels;

/// <summary>
/// The entity relation.
/// </summary>
public class TbEntityRelation
{
    /// <summary>
    /// The from entity id.
    /// </summary>
    public TbEntityId? From { get; set; }

    /// <summary>
    /// The to entity id.
    /// </summary>
    public TbEntityId? To { get; set; }

    /// <summary>
    /// The relation type.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// The additional info.
    /// </summary>
    public object? AdditionalInfo { get; set; }
}