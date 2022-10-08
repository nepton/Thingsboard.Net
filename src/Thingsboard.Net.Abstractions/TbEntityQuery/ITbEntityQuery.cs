using System.Threading;
using System.Threading.Tasks;
using Thingsboard.Net.Common;
using Thingsboard.Net.Models;

namespace Thingsboard.Net.TbEntityQuery;

public interface ITbEntityQuery : IClientApi<ITbEntityQuery>
{
    /// <summary>
    /// Allows to run complex queries over platform entities (devices, assets, customers, etc) based on the combination of main entity filter and multiple key filters. Returns the paginated result of the query that contains requested entity fields and latest values of requested attributes and time-series data.
    /// </summary>
    /// <param name="entityFilter">Entity Filter body depends on the 'type' parameter. Let's review available entity filter types. In fact, they do correspond to available dashboard aliases.</param>
    /// <param name="keyFilter">Key Filter allows you to define complex logical expressions over entity field, attribute or latest time-series value. The filter is defined using 'key', 'valueType' and 'predicate' objects. Single Entity Query may have zero, one or multiple predicates. If multiple filters are defined, they are evaluated using logical 'AND'. The example below checks that temperature of the entity is above 20 degrees:</param>
    /// <param name="entityFields">the field to query</param>
    /// <param name="latestValues"></param>
    /// <param name="pageLink">page control</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<PageCollection<TbEntity>> FindEntityDataByQueryAsync(
        TbEntityFilter        entityFilter,
        TbKeyFilter           keyFilter,
        TbEntityField[]       entityFields,
        TbEntityField[]       latestValues,
        TbEntityDataPageLink pageLink,
        CancellationToken     cancel = default);

    /// <summary>
    /// Allows to run complex queries to search the count of platform entities (devices, assets, customers, etc) based on the combination of main entity filter and multiple key filters. Returns the number of entities that match the query definition.
    /// </summary>
    /// <param name="entityFilter"></param>
    /// <param name="keyFilter"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<int> CountEntityDataByQueryAsync(
        TbEntityFilter    entityFilter,
        TbKeyFilter       keyFilter,
        CancellationToken cancel = default);
}
