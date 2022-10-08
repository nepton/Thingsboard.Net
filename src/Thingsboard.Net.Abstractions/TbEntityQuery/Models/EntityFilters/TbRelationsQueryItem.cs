using Thingsboard.Net.Models;

namespace Thingsboard.Net.TbEntityQuery;

public class TbRelationsQueryItem
{
    public string RelationType { get; }

    public TbEntityType[] EntityTypes { get; }

    public TbRelationsQueryItem(string relationType, TbEntityType[] entityTypes)
    {
        RelationType = relationType;
        EntityTypes  = entityTypes;
    }

    public object ToQuery()
    {
        return new
        {
            relationType = RelationType,
            entityTypes  = EntityTypes
        };
    }
}
