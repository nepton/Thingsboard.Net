using System;
using System.Threading;
using System.Threading.Tasks;

namespace Thingsboard.Net;

/// <summary>
/// Alarm API
/// </summary>
public interface ITbAlarmClient : ITbClient<ITbAlarmClient>
{
    /// <summary>
    /// Creates or Updates the Alarm. When creating alarm, platform generates Alarm Id as time-based UUID. The newly created Alarm id will be present in the response. Specify existing Alarm id to update the alarm. Referencing non-existing Alarm Id will cause 'Not Found' error.
    /// </summary>
    /// <param name="alarm"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbAlarm> SaveAlarmAsync(TbAlarm alarm, CancellationToken cancel = default);

    /// <summary>
    /// Returns a page of alarms for the selected entity. Specifying both parameters 'searchStatus' and 'status' at the same time will cause an error. You can specify parameters to filter the results. The result is wrapped with PageData object that allows you to iterate over result set using pagination. See the 'Model' tab of the Response Class for more details.
    /// </summary>
    /// <returns></returns>
    Task<TbPage<TbAlarm>> GetAlarmsAsync(
        TbEntityType          entityType,
        Guid                  entityId,
        int                   pageSize,
        int                   page,
        TbAlarmSearchStatus?  searchStatus = null,
        TbAlarmStatus?        status       = null,
        string?               textSearch   = null,
        TbAlarmSortProperty?  sortProperty = null,
        TbSortOrder? sortOrder    = null,
        DateTime?             startTime    = null,
        DateTime?             endTime      = null,
        CancellationToken     cancel       = default);

    /// <summary>
    /// Fetch the Alarm object based on the provided Alarm Id. If the user has the authority of 'Tenant Administrator', the server checks that the originator of alarm is owned by the same tenant. If the user has the authority of 'Customer User', the server checks that the originator of alarm belongs to the customer.
    /// </summary>
    /// <param name="alarmId"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbAlarm?> GetAlarmByIdAsync(Guid alarmId, CancellationToken cancel = default);

    /// <summary>
    /// Acknowledge the Alarm. Once acknowledged, the 'ack_ts' field will be set to current timestamp and special rule chain event 'ALARM_ACK' will be generated. Referencing non-existing Alarm Id will cause an error.
    /// </summary>
    /// <param name="tbAlarmId"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task AcknowledgeAlarmAsync(Guid tbAlarmId, CancellationToken cancel = default);

    /// <summary>
    /// Clear the Alarm. Once cleared, the 'clear_ts' field will be set to current timestamp and special rule chain event 'ALARM_CLEAR' will be generated. Referencing non-existing Alarm Id will cause an error.
    /// </summary>
    /// <param name="tbAlarmId"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task ClearAlarmAsync(Guid tbAlarmId, CancellationToken cancel = default);

    /// <summary>
    /// Deletes the Alarm. Referencing non-existing Alarm Id will cause an error.
    /// </summary>
    /// <param name="alarmId"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task DeleteAlarmAsync(Guid alarmId, CancellationToken cancel = default);
}
