namespace Thingsboard.Net.TbEntityRelationModels;

/// <summary>
/// The entity relation.
/// </summary>
public record TbEntityRelation
{
    /// <summary>
    /// The entity relation.
    /// </summary>
    public TbEntityRelation(TbEntityId from, TbEntityId to, string type)
    {
        From = from;
        To   = to;
        Type = type;
    }

    /// <summary>
    /// The from entity id.
    /// </summary>
    public TbEntityId From { get; }

    /// <summary>
    /// The to entity id.
    /// </summary>
    public TbEntityId To { get; }

    /// <summary>
    /// The relation type.
    /// </summary>
    public string Type { get; }

    /// <summary>
    /// The additional info.
    /// </summary>
    public object? AdditionalInfo { get; set; }
}
