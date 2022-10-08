namespace Thingsboard.Net.TbEntityQuery;

/// <summary>
/// The filter condition for entity query
/// </summary>
public abstract class TbEntityFilter
{
    /// <summary>
    /// The type of filter
    /// </summary>
    public abstract string Type { get; }
}
