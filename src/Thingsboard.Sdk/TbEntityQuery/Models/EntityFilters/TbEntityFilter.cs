namespace Thingsboard.Sdk.TbEntityQuery;

/// <summary>
/// The filter condition for entity query
/// </summary>
public abstract class TbEntityFilter
{
    /// <summary>
    /// Convert filter to the object that can be serialized to JSON
    /// </summary>
    /// <returns></returns>
    public abstract object ToQuery();
}
