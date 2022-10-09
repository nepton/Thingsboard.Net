namespace Thingsboard.Net.TbEntityQuery;

/// <summary>
/// Allows to filter entity views based on their type and the 'starts with' expression over their name.
/// For example, this entity filter selects all 'Concrete Mixer' entity views which name starts with 'CAT':
/// </summary>
public class TbEntityViewFilter : TbEntityFilter
{
    public override string Type => "entityViewType";

    public string? EntityViewType { get; }

    /// <summary>
    /// 'starts with' expression over their name
    /// </summary>
    public string? EntityViewNameFilter { get; set; }

    public TbEntityViewFilter()
    {
    }

    public TbEntityViewFilter(string entityViewType, string? entityViewNameStartsWith)
    {
        EntityViewType       = entityViewType;
        EntityViewNameFilter = entityViewNameStartsWith;
    }
}
