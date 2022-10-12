using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Thingsboard.Net.Exceptions;
using Thingsboard.Net.Flurl.Utilities;
using Thingsboard.Net.Flurl.Utilities.Implements;

namespace Thingsboard.Net.Flurl;

public class FlurlTbRpcClient : FlurlTbClient<ITbRpcClient>, ITbRpcClient
{
    private readonly IRequestBuilder _builder;

    public FlurlTbRpcClient(IRequestBuilder builder)
    {
        _builder = builder;
    }

    /// <summary>
    /// Sends the one-way remote-procedure call (RPC) request to device. Sends the one-way remote-procedure call (RPC) request to device. The RPC call is A JSON that contains the method name ('method'), parameters ('params') and multiple optional fields. See example below. We will review the properties of the RPC call one-by-one below.
    /// </summary>
    /// <param name="deviceId">A string value representing the device id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns>In case of persistent RPC, the result of this call is 'rpcId' UUID. In case of lightweight RPC, the result of this call is either 200 OK if the message was sent to device, or 504 Gateway Timeout if device is offline.</returns>
    public Task SendOneWayRpcAsync(Guid deviceId, TbRpcRequest request, CancellationToken cancel = default)
    {
        var builder = _builder.MergeCustomOptions(CustomOptions);

        var policy = builder.GetPolicyBuilder()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackOn(HttpStatusCode.GatewayTimeout, () => throw new TbDeviceRpcException(TbDeviceRpcErrorCode.Timeout))
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            await builder.CreateRequest()
                .AppendPathSegment($"/api/rpc/oneway/{deviceId}")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .PostJsonAsync(request, cancel);
        });
    }

    /// <summary>
    /// Get information about the status of the RPC call.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="rpcId">A string value representing the rpc id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbRpc?> GetPersistentRpcAsync(Guid rpcId, CancellationToken cancel = default)
    {
        var builder = _builder.MergeCustomOptions(CustomOptions);

        var policy = builder.GetPolicyBuilder<TbRpc?>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, null)
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            return await builder.CreateRequest()
                .AppendPathSegment($"/api/rpc/persistent/{rpcId}")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .GetJsonAsync<TbRpc>(cancel);
        });
    }

    /// <summary>
    /// Deletes the persistent RPC request.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="rpcId">A string value representing the rpc id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task DeletePersistentRpcAsync(Guid rpcId, CancellationToken cancel = default)
    {
        var builder = _builder.MergeCustomOptions(CustomOptions);

        var policy = builder.GetPolicyBuilder()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackOn(HttpStatusCode.NotFound, ()=> throw new TbEntityNotFoundException(TbEntityType.RPC, rpcId))
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            await builder.CreateRequest()
                .AppendPathSegment($"/api/rpc/persistent/{rpcId}")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .DeleteAsync(cancel);
        });
    }

    /// <summary>
    /// Allows to query RPC calls for specific device using pagination.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="deviceId">A string value representing the device id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="pageSize">Maximum amount of entities in a one page</param>
    /// <param name="page">Sequence number of page starting from 0</param>
    /// <param name="rpcStatus">Status of the RPC</param>
    /// <param name="sortProperty">Property of entity to sort by</param>
    /// <param name="sortOrder">Sort order. ASC (ASCENDING) or DESC (DESCENDING)</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbPage<TbRpc>> GetPersistentRpcRequestsAsync(
        Guid                    deviceId,
        int                     pageSize,
        int                     page,
        TbRpcStatus?            rpcStatus    = null,
        TbRpcQuerySortProperty? sortProperty = null,
        TbSortOrder?            sortOrder    = null,
        CancellationToken       cancel       = default)
    {
        var builder = _builder.MergeCustomOptions(CustomOptions);

        var policy = builder.GetPolicyBuilder<TbPage<TbRpc>>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, TbPage<TbRpc>.Empty)
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var request = builder.CreateRequest()
                .AppendPathSegment($"/api/rpc/persistent/{deviceId}")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .SetQueryParam("pageSize",     pageSize)
                .SetQueryParam("page",         page)
                .SetQueryParam("rpcStatus",    rpcStatus)
                .SetQueryParam("sortProperty", sortProperty)
                .SetQueryParam("sortOrder",    sortOrder);

            return await request.GetJsonAsync<TbPage<TbRpc>>(cancel);
        });
    }

    /// <summary>
    /// Sends the two-way remote-procedure call (RPC) request to device. Sends the one-way remote-procedure call (RPC) request to device. The RPC call is A JSON that contains the method name ('method'), parameters ('params') and multiple optional fields. See example below. We will review the properties of the RPC call one-by-one below.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="deviceId">A string value representing the device id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="request">The rpc request fields</param>
    /// <param name="cancel"></param>
    /// <returns>In case of persistent RPC, the result of this call is 'rpcId' UUID. In case of lightweight RPC, the result of this call is the response from device, or 504 Gateway Timeout if device is offline.</returns>
    public Task SendTwoWayRpcAsync(Guid deviceId, TbRpcRequest request, CancellationToken cancel = default)
    {
        var builder = _builder.MergeCustomOptions(CustomOptions);

        var policy = builder.GetPolicyBuilder()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackOn(HttpStatusCode.GatewayTimeout, () => throw new TbDeviceRpcException(TbDeviceRpcErrorCode.Timeout))
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            await builder.CreateRequest()
                .AppendPathSegment($"/api/rpc/twoway/{deviceId}")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .PostJsonAsync(request, cancel);
        });
    }
}
