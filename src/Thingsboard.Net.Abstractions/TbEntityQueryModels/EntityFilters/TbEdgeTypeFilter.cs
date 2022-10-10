namespace Thingsboard.Net;

/// <summary>
/// Allows to filter entity views based on their type and the 'starts with' expression over their name.
/// For example, this entity filter selects all 'Concrete Mixer' entity views which name starts with 'CAT':
/// </summary>
public class TbEdgeTypeFilter : TbEntityFilter
{
    public override string Type => "edgeType";

    public string? EdgeType { get; }

    /// <summary>
    /// 'starts with' expression over their name
    /// </summary>
    public string? EdgeNameFilter { get; }

    public TbEdgeTypeFilter()
    {
    }

    public TbEdgeTypeFilter(string edgeType, string? edgeNameFilter)
    {
        EdgeType       = edgeType;
        EdgeNameFilter = edgeNameFilter;
    }
}
