namespace Thingsboard.Net;

/// <summary>
/// Entity filter by entity name
/// </summary>
public class TbEntityNameFilter : TbEntityFilter
{
    public override string Type => "entityName";

    public TbEntityType EntityType { get; set; }

    /// <summary>
    /// Allows to filter entities of the same type using the 'starts with' expression over entity name.
    /// For example, this entity filter selects all devices which name starts with 'Air Quality':
    /// </summary>
    public string? EntityNameFilter { get; set; }

    public TbEntityNameFilter()
    {
    }

    public TbEntityNameFilter(TbEntityType type, string entityName)
    {
        EntityType       = type;
        EntityNameFilter = entityName;
    }
}
