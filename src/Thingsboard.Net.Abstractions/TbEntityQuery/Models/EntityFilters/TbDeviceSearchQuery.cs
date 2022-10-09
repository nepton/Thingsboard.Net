using Thingsboard.Net.Models;

namespace Thingsboard.Net.TbEntityQuery;

/// <summary>
/// Allows to filter devices that are related to the provided root entity. Filters related devices based on the relation type and set of device types.
/// Possible direction values are 'TO' and 'FROM'. The 'maxLevel' defines how many relation levels should the query search 'recursively'.
/// Assuming the 'maxLevel' is > 1, the 'fetchLastLevelOnly' defines either to return all related entities or only entities that are on the last level of relations.
/// The 'relationType' defines the type of the relation to search for. The 'deviceTypes' defines the type of the device to search for.
/// The relation query calculates all related entities, even if they are filtered using different relation types, and then extracts only devices that match 'relationType'
/// and 'deviceTypes' conditions.
/// 
/// For example, this entity filter selects 'Charging port' and 'Air Quality Sensor' devices which are related to the asset with id 'e52b0020-2a7a-11ec-94eb-213c95f54092' using 'Contains' relation:
/// </summary>
public class TbDeviceSearchQuery : TbEntityFilter
{
    /// <summary>
    /// The type of filter
    /// </summary>
    public override string Type => "deviceSearchQuery";

    public TbEntityId? RootEntity { get; set; }

    public TbRelationDirection Direction { get; set; }

    public int MaxLevel { get; set; }

    public bool FetchLastLevelOnly { get; set; }

    public string? RelationType { get; set; }

    public string[]? DeviceTypes { get; set; }

    public TbDeviceSearchQuery()
    {
    }

    public TbDeviceSearchQuery(TbEntityId rootEntity, TbRelationDirection direction, int maxLevel, bool fetchLastLevelOnly, string relationType, string[] deviceTypes)
    {
        RootEntity         = rootEntity;
        Direction          = direction;
        MaxLevel           = maxLevel;
        FetchLastLevelOnly = fetchLastLevelOnly;
        RelationType       = relationType;
        DeviceTypes        = deviceTypes;
    }
}
