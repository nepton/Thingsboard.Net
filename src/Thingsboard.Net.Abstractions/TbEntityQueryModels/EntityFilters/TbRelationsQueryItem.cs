using System;

namespace Thingsboard.Net;

public class TbRelationsQueryItem
{
    public string         RelationType { get; set; } = string.Empty;
    public TbEntityType[] EntityTypes  { get; set; } = Array.Empty<TbEntityType>();

    public TbRelationsQueryItem()
    {
    }

    public TbRelationsQueryItem(string relationType, TbEntityType[] entityTypes)
    {
        RelationType = relationType;
        EntityTypes  = entityTypes;
    }
}
