﻿using System;

namespace Thingsboard.Net;

/// <summary>
/// Allows to filter entities that are related to the provided root entity. Possible direction values are 'TO' and 'FROM'. The 'maxLevel' defines how many relation levels
/// should the query search 'recursively'. Assuming the 'maxLevel' is > 1, the 'fetchLastLevelOnly' defines either to return all related entities or only entities that
/// are on the last level of relations. The 'filter' object allows you to define the relation type and set of acceptable entity types to search for. The relation query
/// calculates all related entities, even if they are filtered using different relation types, and then extracts only those who match the 'filters'.
/// 
/// For example, this entity filter selects all devices and assets which are related to the asset with id 'e51de0c0-2a7a-11ec-94eb-213c95f54092':
/// </summary>
public class TbRelationsQueryFilter : TbEntityFilter
{
    public override string Type => "relationsQuery";

    public TbEntityId             RootEntity         { get; set; } = TbEntityId.Empty;
    public TbRelationDirection    Direction          { get; set; }
    public int                    MaxLevel           { get; set; }
    public bool                   FetchLastLevelOnly { get; set; }
    public TbRelationsQueryItem[] Filters            { get; set; } = Array.Empty<TbRelationsQueryItem>();

    public TbRelationsQueryFilter()
    {
    }

    public TbRelationsQueryFilter(TbEntityId rootEntityId, TbRelationDirection direction, int maxLevel, bool fetchLastLevelOnly, TbRelationsQueryItem[] filters)
    {
        RootEntity         = rootEntityId;
        Direction          = direction;
        MaxLevel           = maxLevel;
        FetchLastLevelOnly = fetchLastLevelOnly;
        Filters            = filters;
    }
}
