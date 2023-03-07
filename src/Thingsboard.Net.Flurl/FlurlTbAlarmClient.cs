using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Thingsboard.Net.Exceptions;
using Thingsboard.Net.Flurl.Utilities;

namespace Thingsboard.Net.Flurl;

public class FlurlTbAlarmClient : FlurlTbClient<ITbAlarmClient>, ITbAlarmClient
{
    public FlurlTbAlarmClient(IRequestBuilder builder) : base(builder)
    {
    }

    /// <summary>
    /// Creates or Updates the Alarm. When creating alarm, platform generates Alarm Id as time-based UUID. The newly created Alarm id will be present in the response. Specify existing Alarm id to update the alarm. Referencing non-existing Alarm Id will cause 'Not Found' error.
    /// </summary>
    /// <param name="alarm"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbAlarm> SaveAlarmAsync(TbAlarm alarm, CancellationToken cancel = default)
    {
        if (alarm == null) throw new ArgumentNullException(nameof(alarm));

        var policy = RequestBuilder.GetPolicyBuilder<TbAlarm>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackOn(HttpStatusCode.NotFound, () => throw new TbEntityNotFoundException(alarm.Id))
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment("api/alarm")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .PostJsonAsync(alarm, cancel)
                .ReceiveJson<TbAlarm>();

            return response;
        });
    }

    /// <summary>
    /// Creates the Alarm. When creating alarm, platform generates Alarm Id as time-based UUID. The newly created Alarm id will be present in the response.
    /// </summary>
    /// <param name="alarm"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbAlarm> SaveAlarmAsync(TbNewAlarm alarm, CancellationToken cancel = default)
    {
        if (alarm == null) throw new ArgumentNullException(nameof(alarm));

        var policy = RequestBuilder.GetPolicyBuilder<TbAlarm>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment("api/alarm")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .PostJsonAsync(alarm, cancel)
                .ReceiveJson<TbAlarm>();

            return response;
        });
    }

    /// <summary>
    /// Returns a page of alarms for the selected entity. Specifying both parameters 'searchStatus' and 'status' at the same time will cause an error. You can specify parameters to filter the results. The result is wrapped with PageData object that allows you to iterate over result set using pagination. See the 'Model' tab of the Response Class for more details.
    /// </summary>
    /// <returns></returns>
    public Task<TbPage<TbAlarm>> GetAlarmsAsync(
        TbEntityType         entityType,
        Guid                 entityId,
        int                  pageSize,
        int                  page,
        bool?                acknowledged = null,
        bool?                cleared      = null,
        string?              textSearch   = null,
        TbAlarmSortProperty? sortProperty = null,
        TbSortOrder?         sortOrder    = null,
        DateTime?            startTime    = null,
        DateTime?            endTime      = null,
        CancellationToken    cancel       = default)
    {
        TbAlarmStatus? status = (cleared, acknowledged) switch
        {
            (true, true)   => TbAlarmStatus.CLEARED_ACK,
            (true, false)  => TbAlarmStatus.CLEARED_UNACK,
            (false, true)  => TbAlarmStatus.ACTIVE_ACK,
            (false, false) => TbAlarmStatus.ACTIVE_UNACK,
            _              => null,
        };

        TbAlarmSearchStatus? searchStatus = (cleared, acknowledged) switch
        {
            (null, true)  => TbAlarmSearchStatus.ACK,
            (null, false) => TbAlarmSearchStatus.UNACK,
            (true, null)  => TbAlarmSearchStatus.CLEARED,
            (false, null) => TbAlarmSearchStatus.ACTIVE,
            (null, null)  => TbAlarmSearchStatus.ANY,
            _             => null,
        };

        var policy = RequestBuilder.GetPolicyBuilder<TbPage<TbAlarm>>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, TbPage<TbAlarm>.Empty)
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var request = builder.CreateRequest()
                .AppendPathSegment($"api/alarm/{entityType}/{entityId}")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .SetQueryParam("pageSize",     pageSize)
                .SetQueryParam("page",         page)
                .SetQueryParam("searchStatus", searchStatus)
                .SetQueryParam("status",       status)
                .SetQueryParam("textSearch",   textSearch)
                .SetQueryParam("sortProperty", sortProperty)
                .SetQueryParam("sortOrder",    sortOrder)
                .SetQueryParam("startTime",    startTime.ToJavaScriptTicks())
                .SetQueryParam("endTime",      endTime.ToJavaScriptTicks());

            return await request.GetJsonAsync<TbPage<TbAlarm>>(cancel);
        });
    }

    /// <summary>
    /// Fetch the Alarm object based on the provided Alarm Id. If the user has the authority of 'Tenant Administrator', the server checks that the originator of alarm is owned by the same tenant. If the user has the authority of 'Customer User', the server checks that the originator of alarm belongs to the customer.
    /// </summary>
    /// <param name="alarmId"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbAlarm?> GetAlarmByIdAsync(Guid alarmId, CancellationToken cancel = default)
    {
        var policy = RequestBuilder.GetPolicyBuilder<TbAlarm?>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, null)
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var result = await builder.CreateRequest()
                .AppendPathSegment($"api/alarm/{alarmId}")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .GetJsonAsync<TbAlarm?>(cancel);

            return result;
        });
    }

    /// <summary>
    /// Acknowledge the Alarm. Once acknowledged, the 'ack_ts' field will be set to current timestamp and special rule chain event 'ALARM_ACK' will be generated. Referencing non-existing Alarm Id will cause an error.
    /// </summary>
    /// <param name="tbAlarmId"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task AcknowledgeAlarmAsync(Guid tbAlarmId, CancellationToken cancel = default)
    {
        var policy = RequestBuilder.GetPolicyBuilder()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackOn(HttpStatusCode.NotFound, () => throw new TbEntityNotFoundException(TbEntityType.ALARM, tbAlarmId))
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            await builder.CreateRequest()
                .AppendPathSegment($"api/alarm/{tbAlarmId}/ack")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .PostJsonAsync(null, cancel);
        });
    }

    /// <summary>
    /// Clear the Alarm. Once cleared, the 'clear_ts' field will be set to current timestamp and special rule chain event 'ALARM_CLEAR' will be generated. Referencing non-existing Alarm Id will cause an error.
    /// </summary>
    /// <param name="tbAlarmId"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task ClearAlarmAsync(Guid tbAlarmId, CancellationToken cancel = default)
    {
        var policy = RequestBuilder.GetPolicyBuilder()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackOn(HttpStatusCode.NotFound, () => throw new TbEntityNotFoundException(TbEntityType.ALARM, tbAlarmId))
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            await builder.CreateRequest()
                .AppendPathSegment($"api/alarm/{tbAlarmId}/clear")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .PostJsonAsync(null, cancel);
        });
    }

    /// <summary>
    /// Deletes the Alarm. Referencing non-existing Alarm Id will cause an error.
    /// </summary>
    /// <param name="alarmId"></param>
    /// <param name="cancel"></param>
    /// <returns>return false if alarm not found</returns>
    Task<bool> ITbAlarmClient.DeleteAlarmAsync(Guid alarmId, CancellationToken cancel)
    {
        var policy = RequestBuilder.GetPolicyBuilder<bool>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, false)
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            await builder.CreateRequest()
                .AppendPathSegment($"api/alarm/{alarmId}")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .DeleteAsync(cancel);

            return true;
        });
    }

    /// <summary>
    /// Returns a page of alarms for the selected entity. Specifying both parameters 'searchStatus' and 'status' at the same time will cause an error. You can specify parameters to filter the results. The result is wrapped with PageData object that allows you to iterate over result set using pagination. See the 'Model' tab of the Response Class for more details.
    /// </summary>
    /// <returns></returns>
    public Task<TbPage<TbAlarm>> GetAllAlarmsAsync(
        int                  pageSize,
        int                  page,
        bool?                acknowledged = null,
        bool?                cleared      = null,
        string?              textSearch   = null,
        TbAlarmSortProperty? sortProperty = null,
        TbSortOrder?         sortOrder    = null,
        DateTime?            startTime    = null,
        DateTime?            endTime      = null,
        CancellationToken    cancel       = default)
    {
        TbAlarmStatus? status = (cleared, acknowledged) switch
        {
            (true, true)   => TbAlarmStatus.CLEARED_ACK,
            (true, false)  => TbAlarmStatus.CLEARED_UNACK,
            (false, true)  => TbAlarmStatus.ACTIVE_ACK,
            (false, false) => TbAlarmStatus.ACTIVE_UNACK,
            _              => null,
        };

        TbAlarmSearchStatus? searchStatus = (cleared, acknowledged) switch
        {
            (null, true)  => TbAlarmSearchStatus.ACK,
            (null, false) => TbAlarmSearchStatus.UNACK,
            (true, null)  => TbAlarmSearchStatus.CLEARED,
            (false, null) => TbAlarmSearchStatus.ACTIVE,
            (null, null)  => TbAlarmSearchStatus.ANY,
            _             => null,
        };

        var policy = RequestBuilder.GetPolicyBuilder<TbPage<TbAlarm>>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, TbPage<TbAlarm>.Empty)
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var request = builder.CreateRequest()
                .AppendPathSegment($"api/alarms")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .SetQueryParam("pageSize",     pageSize)
                .SetQueryParam("page",         page)
                .SetQueryParam("searchStatus", searchStatus)
                .SetQueryParam("status",       status)
                .SetQueryParam("textSearch",   textSearch)
                .SetQueryParam("sortProperty", sortProperty)
                .SetQueryParam("sortOrder",    sortOrder)
                .SetQueryParam("startTime",    startTime.ToJavaScriptTicks())
                .SetQueryParam("endTime",      endTime.ToJavaScriptTicks());

            return await request.GetJsonAsync<TbPage<TbAlarm>>(cancel);
        });
    }
}
