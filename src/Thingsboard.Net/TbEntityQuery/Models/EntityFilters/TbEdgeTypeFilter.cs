namespace Thingsboard.Net.TbEntityQuery;

/// <summary>
/// Allows to filter entity views based on their type and the 'starts with' expression over their name.
/// For example, this entity filter selects all 'Concrete Mixer' entity views which name starts with 'CAT':
/// </summary>
public class TbEdgeTypeFilter : TbEntityFilter
{
    public string EdgeType { get; }

    /// <summary>
    /// 'starts with' expression over their name
    /// </summary>
    public string? EdgeNameStartsWith { get; }

    public TbEdgeTypeFilter(string edgeType, string? edgeNameStartsWith)
    {
        EdgeType           = edgeType;
        EdgeNameStartsWith = edgeNameStartsWith;
    }

    public override object ToQuery()
    {
        return new
        {
            type           = "edgeType",
            edgeType       = EdgeType,
            edgeNameFilter = EdgeNameStartsWith,
        };
    }
}
