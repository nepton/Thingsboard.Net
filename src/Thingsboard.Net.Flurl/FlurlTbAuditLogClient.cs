using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Thingsboard.Net.Flurl.Utilities;
using Thingsboard.Net.Flurl.Utilities.Implements;

namespace Thingsboard.Net.Flurl;

public class FlurlTbAuditLogClient : FlurlTbClient<ITbAuditLogClient>, ITbAuditLogClient
{
    private readonly IRequestBuilder _builder;

    public FlurlTbAuditLogClient(IRequestBuilder builder)
    {
        _builder = builder;
    }

    /// <summary>
    /// Returns a page of audit logs related to all entities in the scope of the current user's Tenant. You can specify parameters to filter the results. The result is wrapped with PageData object that allows you to iterate over result set using pagination. See the 'Model' tab of the Response Class for more details.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="pageSize">Maximum amount of entities in a one page</param>
    /// <param name="page">Sequence number of page starting from 0</param>
    /// <param name="textSearch">The case insensitive 'substring' filter based on one of the next properties: entityType, entityName, userName, actionType, actionStatus.</param>
    /// <param name="sortProperty">Property of audit log to sort by. See the 'Model' tab of the Response Class for more details. Note: entityType sort property is not defined in the AuditLog class, however, it can be used to sort audit logs by types of entities that were logged.</param>
    /// <param name="sortOrder">Sort order. ASC (ASCENDING) or DESC (DESCENDING)</param>
    /// <param name="startTime">The start timestamp in milliseconds of the search time range over the AuditLog class field: 'createdTime'.</param>
    /// <param name="endTime">The end timestamp in milliseconds of the search time range over the AuditLog class field: 'createdTime'.</param>
    /// <param name="actionType">A String value representing comma-separated list of action types. This parameter is optional, but it can be used to filter results to fetch only audit logs of specific action types. For example, 'LOGIN', 'LOGOUT'. See the 'Model' tab of the Response Class for more details.</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbPage<TbAudit>> GetAuditLogsAsync(
        int                          pageSize,
        int                          page,
        string?                      textSearch,
        TbAuditLogQuerySortProperty? sortProperty,
        TbSortOrder?                 sortOrder,
        DateTime?                    startTime,
        DateTime?                    endTime,
        string?                      actionType,
        CancellationToken            cancel = default)
    {
        var builder = _builder.MergeCustomOptions(CustomOptions);

        var policy = builder.GetPolicyBuilder<TbPage<TbAudit>>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, TbPage<TbAudit>.Empty)
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var request = builder.CreateRequest()
                .AppendPathSegment("/api/audit/logs")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .SetQueryParam("pageSize",     pageSize)
                .SetQueryParam("page",         page)
                .SetQueryParam("textSearch",   textSearch)
                .SetQueryParam("sortProperty", sortProperty)
                .SetQueryParam("sortOrder",    sortOrder)
                .SetQueryParam("startTime",    startTime.ToJavaScriptTicks())
                .SetQueryParam("endTime",      endTime.ToJavaScriptTicks())
                .SetQueryParam("actionType",   actionType);

            return await request.GetJsonAsync<TbPage<TbAudit>>(cancel);
        });
    }

    /// <summary>
    /// Returns a page of audit logs related to the targeted customer entities (devices, assets, etc.), and users actions (login, logout, etc.) that belong to this customer. You can specify parameters to filter the results. The result is wrapped with PageData object that allows you to iterate over result set using pagination. See the 'Model' tab of the Response Class for more details.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="customerId">A Guid value representing the customer id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="pageSize">Maximum amount of entities in a one page</param>
    /// <param name="page">Sequence number of page starting from 0</param>
    /// <param name="textSearch">The case insensitive 'substring' filter based on one of the next properties: entityType, entityName, userName, actionType, actionStatus.</param>
    /// <param name="sortProperty">Property of audit log to sort by. See the 'Model' tab of the Response Class for more details. Note: entityType sort property is not defined in the AuditLog class, however, it can be used to sort audit logs by types of entities that were logged.</param>
    /// <param name="sortOrder">Sort order. ASC (ASCENDING) or DESC (DESCENDING)</param>
    /// <param name="startTime">The start timestamp in milliseconds of the search time range over the AuditLog class field: 'createdTime'.</param>
    /// <param name="endTime">The end timestamp in milliseconds of the search time range over the AuditLog class field: 'createdTime'.</param>
    /// <param name="actionType">A String value representing comma-separated list of action types. This parameter is optional, but it can be used to filter results to fetch only audit logs of specific action types. For example, 'LOGIN', 'LOGOUT'. See the 'Model' tab of the Response Class for more details.</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbPage<TbAudit>> GetAuditLogsByCustomerIdAsync(
        Guid                         customerId,
        int                          pageSize,
        int                          page,
        string?                      textSearch,
        TbAuditLogQuerySortProperty? sortProperty,
        TbSortOrder?                 sortOrder,
        DateTime?                    startTime,
        DateTime?                    endTime,
        string?                      actionType,
        CancellationToken            cancel = default)
    {
        var builder = _builder.MergeCustomOptions(CustomOptions);

        var policy = builder.GetPolicyBuilder<TbPage<TbAudit>>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, TbPage<TbAudit>.Empty)
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var request = builder.CreateRequest()
                .AppendPathSegment($"/api/audit/logs/customer/{customerId}")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .SetQueryParam("pageSize",     pageSize)
                .SetQueryParam("page",         page)
                .SetQueryParam("textSearch",   textSearch)
                .SetQueryParam("sortProperty", sortProperty)
                .SetQueryParam("sortOrder",    sortOrder)
                .SetQueryParam("startTime",    startTime.ToJavaScriptTicks())
                .SetQueryParam("endTime",      endTime.ToJavaScriptTicks())
                .SetQueryParam("actionType",   actionType);

            return await request.GetJsonAsync<TbPage<TbAudit>>(cancel);
        });
    }

    /// <summary>
    /// Returns a page of audit logs related to the actions on the targeted entity. Basically, this API call is used to get the full lifecycle of some specific entity. For example to see when a device was created, updated, assigned to some customer, or even deleted from the system. You can specify parameters to filter the results. The result is wrapped with PageData object that allows you to iterate over result set using pagination. See the 'Model' tab of the Response Class for more details.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="entityType">A string value representing the entity type. For example, 'DEVICE'</param>
    /// <param name="entityId">A string value representing the entity id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="pageSize">Maximum amount of entities in a one page</param>
    /// <param name="page">Sequence number of page starting from 0</param>
    /// <param name="textSearch">The case insensitive 'substring' filter based on one of the next properties: entityType, entityName, userName, actionType, actionStatus.</param>
    /// <param name="sortProperty">Property of audit log to sort by. See the 'Model' tab of the Response Class for more details. Note: entityType sort property is not defined in the AuditLog class, however, it can be used to sort audit logs by types of entities that were logged.</param>
    /// <param name="sortOrder">Sort order. ASC (ASCENDING) or DESC (DESCENDING)</param>
    /// <param name="startTime">The start timestamp in milliseconds of the search time range over the AuditLog class field: 'createdTime'.</param>
    /// <param name="endTime">The end timestamp in milliseconds of the search time range over the AuditLog class field: 'createdTime'.</param>
    /// <param name="actionType">A String value representing comma-separated list of action types. This parameter is optional, but it can be used to filter results to fetch only audit logs of specific action types. For example, 'LOGIN', 'LOGOUT'. See the 'Model' tab of the Response Class for more details.</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbPage<TbAudit>> GetAuditLogsByEntityIdAsync(
        TbEntityType                 entityType,
        Guid                         entityId,
        int                          pageSize,
        int                          page,
        string?                      textSearch,
        TbAuditLogQuerySortProperty? sortProperty,
        TbSortOrder?                 sortOrder,
        DateTime?                    startTime,
        DateTime?                    endTime,
        string?                      actionType,
        CancellationToken            cancel = default)
    {
        var builder = _builder.MergeCustomOptions(CustomOptions);

        var policy = builder.GetPolicyBuilder<TbPage<TbAudit>>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, TbPage<TbAudit>.Empty)
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var request = builder.CreateRequest()
                .AppendPathSegment($"/api/audit/logs/entity/{entityType}/{entityId}")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .SetQueryParam("pageSize",     pageSize)
                .SetQueryParam("page",         page)
                .SetQueryParam("textSearch",   textSearch)
                .SetQueryParam("sortProperty", sortProperty)
                .SetQueryParam("sortOrder",    sortOrder)
                .SetQueryParam("startTime",    startTime.ToJavaScriptTicks())
                .SetQueryParam("endTime",      endTime.ToJavaScriptTicks())
                .SetQueryParam("actionType",   actionType);

            return await request.GetJsonAsync<TbPage<TbAudit>>(cancel);
        });
    }

    /// <summary>
    /// Returns a page of audit logs related to the actions of targeted user. For example, RPC call to a particular device, or alarm acknowledgment for a specific device, etc. You can specify parameters to filter the results. The result is wrapped with PageData object that allows you to iterate over result set using pagination. See the 'Model' tab of the Response Class for more details.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="userId">A string value representing the user id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="pageSize">Maximum amount of entities in a one page</param>
    /// <param name="page">Sequence number of page starting from 0</param>
    /// <param name="textSearch">The case insensitive 'substring' filter based on one of the next properties: entityType, entityName, userName, actionType, actionStatus.</param>
    /// <param name="sortProperty">Property of audit log to sort by. See the 'Model' tab of the Response Class for more details. Note: entityType sort property is not defined in the AuditLog class, however, it can be used to sort audit logs by types of entities that were logged.</param>
    /// <param name="sortOrder">Sort order. ASC (ASCENDING) or DESC (DESCENDING)</param>
    /// <param name="startTime">The start timestamp in milliseconds of the search time range over the AuditLog class field: 'createdTime'.</param>
    /// <param name="endTime">The end timestamp in milliseconds of the search time range over the AuditLog class field: 'createdTime'.</param>
    /// <param name="actionType">A String value representing comma-separated list of action types. This parameter is optional, but it can be used to filter results to fetch only audit logs of specific action types. For example, 'LOGIN', 'LOGOUT'. See the 'Model' tab of the Response Class for more details.</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbPage<TbAudit>> GetAuditLogsByUserIdAsync(
        Guid                         userId,
        int                          pageSize,
        int                          page,
        string?                      textSearch,
        TbAuditLogQuerySortProperty? sortProperty,
        TbSortOrder?                 sortOrder,
        DateTime?                    startTime,
        DateTime?                    endTime,
        string?                      actionType,
        CancellationToken            cancel = default)
    {
        var builder = _builder.MergeCustomOptions(CustomOptions);

        var policy = builder.GetPolicyBuilder<TbPage<TbAudit>>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, TbPage<TbAudit>.Empty)
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var request = builder.CreateRequest()
                .AppendPathSegment($"/api/audit/logs/user/{userId}")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .SetQueryParam("pageSize",     pageSize)
                .SetQueryParam("page",         page)
                .SetQueryParam("textSearch",   textSearch)
                .SetQueryParam("sortProperty", sortProperty)
                .SetQueryParam("sortOrder",    sortOrder)
                .SetQueryParam("startTime",    startTime.ToJavaScriptTicks())
                .SetQueryParam("endTime",      endTime.ToJavaScriptTicks())
                .SetQueryParam("actionType",   actionType);

            return await request.GetJsonAsync<TbPage<TbAudit>>(cancel);
        });
    }
}
