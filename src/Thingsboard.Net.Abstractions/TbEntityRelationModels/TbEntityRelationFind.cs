namespace Thingsboard.Net.TbEntityRelationModels;

/// <summary>
/// The entity relation query
/// </summary>
public class TbEntityRelationFind
{
    /// <summary>
    /// The filters.
    /// </summary>
    public TbEntityRelationFindFilter[]? Filters { get; set; }

    /// <summary>
    /// The parameters.
    /// </summary>
    public TbEntityRelationFindParameter? Parameters { get; set; }
}