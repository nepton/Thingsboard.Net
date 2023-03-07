namespace Thingsboard.Net.TbEntityRelationModels;

/// <summary>
/// The entity relation.
/// </summary>
public class TbEntityRelation
{
    public TbEntityRelation()
    {
    }

    public TbEntityRelation(TbEntityId from, TbEntityId to, string type)
    {
        From = from;
        To   = to;
        Type = type;
    }

    /// <summary>
    /// The from entity id.
    /// </summary>
    public TbEntityId From { get; private set; } = TbEntityId.Empty;

    /// <summary>
    /// The to entity id.
    /// </summary>
    public TbEntityId To { get; private set; } = TbEntityId.Empty;

    /// <summary>
    /// The relation type.
    /// </summary>
    public string Type { get; private set; } = string.Empty;

    /// <summary>
    /// The additional info.
    /// </summary>
    public object? AdditionalInfo { get; set; }
}
