namespace Thingsboard.Sdk.TbEntityQuery;

/// <summary>
/// Allows to filter entity views based on their type and the 'starts with' expression over their name.
/// For example, this entity filter selects all 'Concrete Mixer' entity views which name starts with 'CAT':
/// </summary>
public class TbEntityViewFilter : TbEntityFilter
{
    public string EntityViewType { get; }

    /// <summary>
    /// 'starts with' expression over their name
    /// </summary>
    public string? EntityViewNameStartsWith { get; }

    public TbEntityViewFilter(string entityViewType, string? entityViewNameStartsWith)
    {
        EntityViewType           = entityViewType;
        EntityViewNameStartsWith = entityViewNameStartsWith;
    }

    public override object ToQuery()
    {
        return new
        {
            type                 = "entityViewType",
            entityViewType       = EntityViewType,
            entityViewNameFilter = EntityViewNameStartsWith
        };
    }
}
