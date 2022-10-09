using Thingsboard.Net.Models;

namespace Thingsboard.Net.TbEntityQuery;

public class TbRelationsQueryItem
{
    public string?         RelationType { get; }
    public TbEntityType[]? EntityTypes  { get; }

    public TbRelationsQueryItem()
    {
    }

    public TbRelationsQueryItem(string relationType, TbEntityType[] entityTypes)
    {
        RelationType = relationType;
        EntityTypes  = entityTypes;
    }
}
