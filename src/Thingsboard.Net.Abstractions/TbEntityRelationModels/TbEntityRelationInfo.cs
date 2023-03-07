namespace Thingsboard.Net.TbEntityRelationModels;

/// <summary>
/// The entity relation info.
/// </summary>
public class TbEntityRelationInfo
{
    /// <summary>
    /// The from entity id.
    /// </summary>
    public TbEntityId? From { get; set; }

    /// <summary>
    /// The from entity name.
    /// </summary>
    public string? FromName { get; set; }

    /// <summary>
    /// The to entity id.
    /// </summary>
    public TbEntityId? To { get; set; }

    /// <summary>
    /// The to entity name.
    /// </summary>
    public string? ToName { get; set; }

    /// <summary>
    /// The relation type.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// The additional info.
    /// </summary>
    public object? AdditionalInfo { get; set; }
}