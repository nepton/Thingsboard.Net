using System.Threading;
using System.Threading.Tasks;

namespace Thingsboard.Net;

public interface ITbEntityQueryClient : ITbClient<ITbEntityQueryClient>
{
    /// <summary>
    /// Allows to run complex queries over platform entities (devices, assets, customers, etc) based on the combination of main entity filter and multiple key filters. Returns the paginated result of the query that contains requested entity fields and latest values of requested attributes and time-series data.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbPage<TbFindEntityDataResponse>> FindEntityDataByQueryAsync(TbFindEntityDataRequest request, CancellationToken cancel = default);

    /// <summary>
    /// Allows to run complex queries to search the count of platform entities (devices, assets, customers, etc) based on the combination of main entity filter and multiple key filters. Returns the number of entities that match the query definition.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<int> CountEntityDataByQueryAsync(TbCountEntityDataRequest request, CancellationToken cancel = default);

    /// <summary>
    /// Returns the number of alarms that match the query definition.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<int> CountAlarmsByQueryAsync(TbAlarmCountQueryRequest request, CancellationToken cancel = default);

    /// <summary>
    /// This method description defines how Alarm Data Query extends the Entity Data Query. See method 'Find Entity Data by Query' first to get the info about 'Entity Data Query'.
    /// </summary>
    /// <remarks>
    /// The platform will first search the entities that match the entity and key filters. Then, the platform will use 'Alarm Page Link' to filter the alarms related to those entities. Finally, platform fetch the properties of alarm that are defined in the 'alarmFields' and combine them with the other entity, attribute and latest time-series fields to return the result.
    /// See example of the alarm query below. The query will search first 100 active alarms with type 'Temperature Alarm' or 'Fire Alarm' for any device with current temperature > 0. The query will return combination of the entity fields: name of the device, device model and latest temperature reading and alarms fields: createdTime, type, severity and status:
    /// </remarks>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbPage<TbAlarmDataQueryResponse>> FindAlarmsByQueryAsync(TbAlarmDataQueryRequest request, CancellationToken cancel = default);
}