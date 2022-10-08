using Thingsboard.Net.Models;

namespace Thingsboard.Net.TbEntityQuery;

/// <summary>
/// Allows to filter edge instances that are related to the provided root entity. Filters related edge instances based on the relation type and set of edge types.
/// Possible direction values are 'TO' and 'FROM'. The 'maxLevel' defines how many relation levels should the query search 'recursively'.
/// Assuming the 'maxLevel' is > 1, the 'fetchLastLevelOnly' defines either to return all related entities or only entities that are on the last level of relations.
/// The 'relationType' defines the type of the relation to search for. The 'deviceTypes' defines the type of the device to search for.
/// The relation query calculates all related entities, even if they are filtered using different relation types, and then extracts only devices that match 'relationType' and 'deviceTypes' conditions.
///
/// For example, this entity filter selects 'Factory' edge instances which are related to the asset with id 'e52b0020-2a7a-11ec-94eb-213c95f54092' using 'Contains' relation:
/// </summary>
public class TbEdgeSearchQuery : TbEntityFilter
{
    public override string Type => "deviceSearchQuery";

    public TbEntityId          RootEntityId       { get; }
    public TbRelationDirection Direction          { get; }
    public int                 MaxLevel           { get; }
    public bool                FetchLastLevelOnly { get; }
    public string              RelationType       { get; }
    public string[]            EdgeTypes          { get; }

    public TbEdgeSearchQuery(TbEntityId rootEntityId,
        TbRelationDirection             direction,
        int                             maxLevel,
        bool                            fetchLastLevelOnly,
        string                          relationType,
        string[]                        edgeTypes)
    {
        RootEntityId       = rootEntityId;
        Direction          = direction;
        MaxLevel           = maxLevel;
        FetchLastLevelOnly = fetchLastLevelOnly;
        RelationType       = relationType;
        EdgeTypes          = edgeTypes;
    }

    public override object ToQuery()
    {
        return new
        {
            rootEntity         = RootEntityId,
            direction          = Direction,
            maxLevel           = MaxLevel,
            fetchLastLevelOnly = FetchLastLevelOnly,
            relationType       = RelationType,
            edgeTypes          = EdgeTypes
        };
    }
}
