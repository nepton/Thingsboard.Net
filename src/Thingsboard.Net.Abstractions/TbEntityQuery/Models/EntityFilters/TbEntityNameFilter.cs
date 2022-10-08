using Thingsboard.Net.Models;

namespace Thingsboard.Net.TbEntityQuery;

/// <summary>
/// Entity filter by entity name
/// </summary>
public class TbEntityNameFilter : TbEntityFilter
{
    public override string Type =>
    public TbEntityType Type { get; }

    /// <summary>
    /// Allows to filter entities of the same type using the 'starts with' expression over entity name.
    /// For example, this entity filter selects all devices which name starts with 'Air Quality':
    /// </summary>
    public string EntityNameStartsWith { get; }

    public TbEntityNameFilter(TbEntityType type, string entityName)
    {
        Type                 = type;
        EntityNameStartsWith = entityName;
    }

    public override object ToQuery()
    {
        return new
        {
            type             = "entityName",
            entityType       = Type,
            entityNameFilter = EntityNameStartsWith
        };
    }
}
