using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Thingsboard.Net.Flurl.Utilities;
using Thingsboard.Net.Flurl.Utilities.Implements;

namespace Thingsboard.Net.Flurl;

public class FlurlTbAlarmClient : FlurlTbClient<ITbAlarmClient>, ITbAlarmClient
{
    private readonly IRequestBuilder _builder;

    public FlurlTbAlarmClient(IRequestBuilder builder)
    {
        _builder = builder;
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

        var builder = _builder.MergeCustomOptions(CustomOptions);

        var policy = builder.GetPolicyBuilder<TbAlarm>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async () =>
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
        TbAlarmSearchStatus? searchStatus = null,
        TbAlarmStatus?       status       = null,
        string?              textSearch   = null,
        TbAlarmSortProperty? sortProperty = null,
        TbSortOrder?         sortOrder    = null,
        DateTime?            startTime    = null,
        DateTime?            endTime      = null,
        CancellationToken    cancel       = default)
    {
        var builder = _builder.MergeCustomOptions(CustomOptions);

        var policy = builder.GetPolicyBuilder<TbPage<TbAlarm>>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, TbPage<TbAlarm>.Empty)
            .Build();

        return policy.ExecuteAsync(async () =>
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
                .SetQueryParam("startTime",    startTime)
                .SetQueryParam("endTime",      endTime);

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
        var builder = _builder.MergeCustomOptions(CustomOptions);

        var policy = builder.GetPolicyBuilder<TbAlarm?>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, null)
            .Build();

        return policy.ExecuteAsync(async () =>
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
        var builder = _builder.MergeCustomOptions(CustomOptions);

        var policy = builder.GetPolicyBuilder()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async () =>
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
        var builder = _builder.MergeCustomOptions(CustomOptions);

        var policy = builder.GetPolicyBuilder()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async () =>
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
    /// <returns></returns>
    public Task DeleteAlarmAsync(Guid alarmId, CancellationToken cancel = default)
    {
        var builder = _builder.MergeCustomOptions(CustomOptions);

        var policy = builder.GetPolicyBuilder()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            await builder.CreateRequest()
                .AppendPathSegment($"api/alarm/{alarmId}")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .DeleteAsync(cancel);
        });
    }
}
