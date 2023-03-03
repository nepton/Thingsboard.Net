namespace Thingsboard.Net;

/// <summary>
/// Allows to filter assets that are related to the provided root entity. Filters related assets based on the relation type and set of asset types. Possible direction values are 'TO' and 'FROM'. The 'maxLevel' defines how many relation levels should the query search 'recursively'. Assuming the 'maxLevel' is > 1, the 'fetchLastLevelOnly' defines either to return all related entities or only entities that are on the last level of relations. The 'relationType' defines the type of the relation to search for. The 'assetTypes' defines the type of the asset to search for. The relation query calculates all related entities, even if they are filtered using different relation types, and then extracts only assets that match 'relationType' and 'assetTypes' conditions.
/// 
/// For example, this entity filter selects 'charging station' assets which are related to the asset with id 'e51de0c0-2a7a-11ec-94eb-213c95f54092' using 'Contains' relation:
/// </summary>
public class TbAssetSearchQuery : TbEntityFilter
{
    /// <summary>
    /// The type of filter
    /// </summary>
    public override string Type => "assetSearchQuery";

    public TbEntityId? RootEntity { get; set; }

    public TbRelationDirection Direction { get; set; }

    public int MaxLevel { get; set; }

    public bool FetchLastLevelOnly { get; set; }

    public string? RelationType { get; set; }

    public string[]? AssetTypes { get; set; }

    public TbAssetSearchQuery()
    {
    }

    public TbAssetSearchQuery(TbEntityId rootEntity, TbRelationDirection direction, int maxLevel, bool fetchLastLevelOnly, string relationType, string[] assetTypes)
    {
        RootEntity         = rootEntity;
        Direction          = direction;
        MaxLevel           = maxLevel;
        FetchLastLevelOnly = fetchLastLevelOnly;
        RelationType       = relationType;
        AssetTypes         = assetTypes;
    }
}
