using System;
using System.Threading;
using System.Threading.Tasks;

namespace Thingsboard.Net.TbDeviceRpc;

/// <summary>
/// TB 系统的 RESTFUL API
/// </summary>
public interface ITbDeviceRpcService
{
    /// <summary>
    /// 向设备发送RPC消息
    /// </summary>
    /// <param name="deviceId">设备Id</param>
    /// <param name="method"></param>
    /// <param name="params"></param>
    /// <param name="additionalInfo">optional, defines metadata for the persistent RPC that will be added to the persistent RPC events.</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task PostDeviceOneWayRPCAsync(Guid deviceId, string method, object? @params, object? additionalInfo, CancellationToken cancel = default);

    /// <summary>
    /// 向设备发送RPC消息
    /// </summary>
    /// <param name="deviceId">设备Id</param>
    /// <param name="method"></param>
    /// <param name="params"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task PostDeviceOneWayRPCAsync(Guid deviceId, string method, object? @params, CancellationToken cancel = default)
    {
        return PostDeviceOneWayRPCAsync(deviceId, method, @params, null, cancel);
    }
}
