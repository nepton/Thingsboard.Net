using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace Thingsboard.Net;

public interface ITbTelemetryClient : ITbClient<ITbTelemetryClient>
{
    /// <summary>
    /// Creates or updates the device attributes based on device id and specified attribute scope. The request payload is a JSON object with key-value format of attributes to create or update. For example:
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="deviceId">A string value representing the device id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="scope">A string value representing the attributes scope. For example, 'SERVER_SCOPE'.</param>
    /// <param name="value"> The request payload is a JSON object with key-value format of attributes to create or update.For example:
    /// {
    ///     "stringKey":"value1", 
    ///     "booleanKey":true, 
    ///     "doubleKey":42.0, 
    ///     "longKey":73, 
    ///     "jsonKey": {
    ///         "someNumber": 42,
    ///         "someArray": [1,2,3],
    ///         "someNestedObject": {"key": "value"}
    ///     }
    /// }
    /// </param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task SaveDeviceAttributesAsync(
        Guid              deviceId,
        TbAttributeScope  scope,
        object            value,
        CancellationToken cancel = default);

    /// <summary>
    /// Delete device attributes using provided Device Id, scope and a list of keys. Referencing a non-existing Device Id will cause an error
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="deviceId">A string value representing the device id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="scope">A string value representing the attributes scope. For example, 'SERVER_SCOPE'.</param>
    /// <param name="keys">A string value representing the comma-separated list of attributes keys. For example, 'active,inactivityAlarmTime'.</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task DeleteDeviceAttributesAsync(
        Guid              deviceId,
        TbAttributeScope  scope,
        string[]          keys,
        CancellationToken cancel = default);

    /// <summary>
    /// V2
    /// Creates or updates the entity attributes based on Entity Id and the specified attribute scope. List of possible attribute scopes depends on the entity type:
    ///     SERVER_SCOPE - supported for all entity types;
    ///     CLIENT_SCOPE - supported for devices;
    ///     SHARED_SCOPE - supported for devices.
    /// 
    /// Referencing a non-existing entity Id or invalid entity type will cause an error.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="entityType">A string value representing the entity type. For example, 'DEVICE'</param>
    /// <param name="entityId">A string value representing the entity id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="scope">A string value representing the attributes scope. For example, 'SERVER_SCOPE'.</param>
    /// <param name="value"> The request payload is a JSON object with key-value format of attributes to create or update.For example:
    /// {
    ///     "stringKey":"value1", 
    ///     "booleanKey":true, 
    ///     "doubleKey":42.0, 
    ///     "longKey":73, 
    ///     "jsonKey": {
    ///         "someNumber": 42,
    ///         "someArray": [1,2,3],
    ///         "someNestedObject": {"key": "value"}
    ///     }
    /// }
    /// </param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task SaveEntityAttributesAsync(
        TbEntityType      entityType,
        Guid              entityId,
        TbAttributeScope  scope,
        object            value,
        CancellationToken cancel = default);

    /// <summary>
    /// Delete entity attributes using provided Entity Id, scope and a list of keys. Referencing a non-existing entity Id or invalid entity type will cause an error.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="entityType">A string value representing the entity type. For example, 'DEVICE'</param>
    /// <param name="entityId">A string value representing the entity id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="scope">A string value representing the attributes scope. For example, 'SERVER_SCOPE'.</param>
    /// <param name="keys">A string value representing the comma-separated list of attributes keys. For example, 'active,inactivityAlarmTime'.</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task DeleteEntityAttributesAsync(
        TbEntityType      entityType,
        Guid              entityId,
        TbAttributeScope  scope,
        string[]          keys,
        CancellationToken cancel = default);

    /// <summary>
    /// Returns a set of unique attribute key names for the selected entity and attributes scope:
    ///     SERVER_SCOPE - supported for all entity types;
    ///     CLIENT_SCOPE - supported for devices;
    ///     SHARED_SCOPE - supported for devices.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="entityType">A string value representing the entity type. For example, 'DEVICE'</param>
    /// <param name="entityId">A string value representing the entity id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<string[]> GetAttributeKeysAsync(
        TbEntityType      entityType,
        Guid              entityId,
        CancellationToken cancel = default);

    /// <summary>
    /// Returns a set of unique attribute key names for the selected entity and attributes scope:
    ///     SERVER_SCOPE - supported for all entity types;
    ///     CLIENT_SCOPE - supported for devices;
    ///     SHARED_SCOPE - supported for devices.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="entityType">A string value representing the entity type. For example, 'DEVICE'</param>
    /// <param name="entityId">A string value representing the entity id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="scope">A string value representing the attributes scope. For example, 'SERVER_SCOPE'.</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<string[]> GetAttributeKeysByScopeAsync(
        TbEntityType      entityType,
        Guid              entityId,
        TbAttributeScope  scope,
        CancellationToken cancel = default);

    /// <summary>
    /// Returns a set of unique time-series key names for the selected entity.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="entityType">A string value representing the entity type. For example, 'DEVICE'</param>
    /// <param name="entityId">A string value representing the entity id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<string[]> GetTimeSeriesKeysAsync(
        TbEntityType      entityType,
        Guid              entityId,
        CancellationToken cancel = default);

    /// <summary>
    /// Creates or updates the entity time-series data based on the Entity Id and request payload.The request payload is a JSON document with three possible formats:
    /// {"temperature": 26}
    /// Single JSON object with timestamp:
    /// {"ts":1634712287000,"values":{"temperature":26, "humidity":87}}
    /// JSON array with timestamps:
    /// [ {"ts":1634712287000,"values":{"temperature":26, "humidity":87}}, {"ts":1634712588000,"values":{"temperature":25, "humidity":88}}]
    /// 
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="entityType">A string value representing the entity type. For example, 'DEVICE'</param>
    /// <param name="entityId">A string value representing the entity id. For example, '784f394c-42b6-435a-983c-b7beff2784f9</param>
    /// <param name="telemetry">Ref summary</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task SaveEntityTimeSeriesAsync(
        TbEntityType      entityType,
        Guid              entityId,
        object            telemetry,
        CancellationToken cancel = default);

    /// <summary>
    /// Creates or updates the entity time-series data based on the Entity Id and request payload.The request payload is a JSON document with three possible formats:
    /// {"temperature": 26}
    /// Single JSON object with timestamp:
    /// {"ts":1634712287000,"values":{"temperature":26, "humidity":87}}
    /// JSON array with timestamps:
    /// [ {"ts":1634712287000,"values":{"temperature":26, "humidity":87}}, {"ts":1634712588000,"values":{"temperature":25, "humidity":88}}]
    /// 
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="entityType">A string value representing the entity type. For example, 'DEVICE'</param>
    /// <param name="entityId">A string value representing the entity id. For example, '784f394c-42b6-435a-983c-b7beff2784f9</param>
    /// <param name="ttl">A long value representing TTL (Time to Live) parameter.</param>
    /// <param name="telemetry">Ref summary</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task SaveEntityTimeSeriesWithTtlAsync(
        TbEntityType      entityType,
        Guid              entityId,
        long              ttl,
        object            telemetry,
        CancellationToken cancel = default);

    /// <summary>
    /// Delete ALL time-series for selected entity based on entity id, entity type and keys. Use 'rewriteLatestIfDeleted' to rewrite latest value (stored in separate table for performance) after deletion of the time range.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="entityType">A string value representing the entity type. For example, 'DEVICE'</param>
    /// <param name="entityId">A string value representing the entity id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="keys">A string list of telemetry keys. If keys are not selected, the result will return all latest timeseries. For example, 'temperature,humidity'.</param>
    /// <param name="rewriteLatestIfDeleted">If the parameter is set to true, the latest telemetry will be rewritten in case that current latest value was removed, otherwise, in case that parameter is set to false the new latest value will not set.</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task DeleteEntityTimeSeriesAsync(
        TbEntityType      entityType,
        Guid              entityId,
        string[]          keys,
        bool?             rewriteLatestIfDeleted = null,
        CancellationToken cancel                 = default);

    /// <summary>
    /// Delete time-series for selected entity based on entity id, entity type and keys. Use 'startTs' and 'endTs' to specify time-range instead. Use 'rewriteLatestIfDeleted' to rewrite latest value (stored in separate table for performance) after deletion of the time range.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="entityType">A string value representing the entity type. For example, 'DEVICE'</param>
    /// <param name="entityId">A string value representing the entity id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="keys">A string list of telemetry keys. If keys are not selected, the result will return all latest timeseries. For example, 'temperature,humidity'.</param>
    /// <param name="startTs">A datetime value representing the start timestamp of removal time range in milliseconds.</param>
    /// <param name="endTs">A datetime value representing the end timestamp of removal time range in milliseconds.</param>
    /// <param name="rewriteLatestIfDeleted">If the parameter is set to true, the latest telemetry will be rewritten in case that current latest value was removed, otherwise, in case that parameter is set to false the new latest value will not set.</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task DeleteEntityTimeSeriesAsync(
        TbEntityType      entityType,
        Guid              entityId,
        string[]          keys,
        DateTime          startTs,
        DateTime          endTs,
        bool?             rewriteLatestIfDeleted = null,
        CancellationToken cancel                 = default);

    /// <summary>
    /// Returns all attributes that belong to specified entity. Use optional 'keys' parameter to return specific attributes. Example of the result:
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="entityType">A string value representing the entity type. For example, 'DEVICE'</param>
    /// <param name="entityId">A string value representing the entity id. For example, '784f394c-42b6-435a-983c-b7beff2784f9</param>
    /// <param name="keys">A string list of attributes keys. For example, 'active,inactivityAlarmTime'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbEntityLatestValue[]> GetAttributesAsync(
        TbEntityType      entityType,
        Guid              entityId,
        string[]          keys,
        CancellationToken cancel = default);

    /// <summary>
    /// Returns all attributes that belong to specified entity. Use optional 'keys' parameter to return specific attributes. Example of the result:
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="entityType">A string value representing the entity type. For example, 'DEVICE'</param>
    /// <param name="entityId">A string value representing the entity id. For example, '784f394c-42b6-435a-983c-b7beff2784f9</param>
    /// <param name="scope">A enum value representing the attributes scope. For example, 'SERVER_SCOPE'.</param>
    /// <param name="keys">A string list of attributes keys. For example, 'active,inactivityAlarmTime'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbEntityLatestValue[]> GetAttributesByScopeAsync(
        TbEntityType      entityType,
        Guid              entityId,
        TbAttributeScope  scope,
        string[]          keys,
        CancellationToken cancel = default);

    /// <summary>
    /// Returns a range of time-series values for specified entity. Use aggregation function ('agg') and aggregation interval ('interval') to enable aggregation of the results on the database / server side. The aggregation is generally more efficient then fetching all records.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="entityType">A string value representing the entity type. For example, 'DEVICE'</param>
    /// <param name="entityId">A string value representing the entity id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="keys">A string list of telemetry keys.</param>
    /// <param name="startTs">A datetime value representing the start timestamp of the time range in milliseconds, UTC.</param>
    /// <param name="endTs">A datetime value representing the end timestamp of the time range in milliseconds, UTC.</param>
    /// <param name="agg">A enum value representing the aggregation function. If the interval is not specified, 'agg' parameter will use 'NONE' value.</param>
    /// <param name="interval">A long value representing the aggregation interval range in milliseconds.</param>
    /// <param name="orderBy">Sort order. ASC (ASCENDING) or DESC (DESCENDING)</param>
    /// <param name="useStrictDataTypes">Enables/disables conversion of telemetry values to strings. Conversion is enabled by default. Set parameter to 'true' in order to disable the conversion.</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<ReadOnlyDictionary<TbEntityField, TbEntityTsValue[]>> GetTimeSeriesAsync(
        TbEntityType          entityType,
        Guid                  entityId,
        string[]              keys,
        DateTime              startTs,
        DateTime              endTs,
        TbTimeSeriesAggregate agg,
        long?                 interval           = null,
        TbSortOrder?          orderBy            = null,
        bool?                 useStrictDataTypes = null,
        CancellationToken     cancel             = default);

    /// <summary>
    /// Returns a range of time-series values for specified entity. Returns not aggregated data by default.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="entityType">A string value representing the entity type. For example, 'DEVICE'</param>
    /// <param name="entityId">A string value representing the entity id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="keys">A string list of telemetry keys.</param>
    /// <param name="startTs">A datetime value representing the start timestamp of the time range in milliseconds, UTC.</param>
    /// <param name="endTs">A datetime value representing the end timestamp of the time range in milliseconds, UTC.</param>
    /// <param name="limit">An integer value that represents a max number of timeseries data points to fetch. This parameter is used only in the case if 'agg' parameter is set to 'NONE'.</param>
    /// <param name="orderBy">Sort order. ASC (ASCENDING) or DESC (DESCENDING)</param>
    /// <param name="useStrictDataTypes">Enables/disables conversion of telemetry values to strings. Conversion is enabled by default. Set parameter to 'true' in order to disable the conversion.</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<ReadOnlyDictionary<TbEntityField, TbEntityTsValue[]>> GetTimeSeriesAsync(
        TbEntityType      entityType,
        Guid              entityId,
        string[]          keys,
        DateTime          startTs,
        DateTime          endTs,
        int?              limit              = null,
        TbSortOrder?      orderBy            = null,
        bool?             useStrictDataTypes = null,
        CancellationToken cancel             = default);

    /// <summary>
    /// Returns all time-series that belong to specified entity. Use optional 'keys' parameter to return specific time-series. The result is a JSON object. The format of the values depends on the 'useStrictDataTypes' parameter. By default, all time-series values are converted to strings:
    /// </summary>
    /// <param name="entityType">A string value representing the entity type. For example, 'DEVICE'</param>
    /// <param name="entityId">A string value representing the entity id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="keys">A string list of telemetry keys. If keys are not selected, the result will return all latest timeseries.</param>
    /// <param name="useStrictDataTypes">Enables/disables conversion of telemetry values to strings. Conversion is enabled by default. Set parameter to 'true' in order to disable the conversion.</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<ReadOnlyDictionary<TbEntityField, TbEntityTsValue>> GetLatestTimeSeriesAsync(
        TbEntityType      entityType,
        Guid              entityId,
        string[]?         keys,
        bool?             useStrictDataTypes = null,
        CancellationToken cancel             = default);
}
