using Thingsboard.Net;

namespace UnitTest.Thingsboard.Net.Flurl.TbRpcClient;

public class RpcUtility
{
    public static async Task<TbEntityId> SendPersistentOneWayRpcAsync(Guid deviceId)
    {
        var client   = TbTestFactory.Instance.CreateRpcClient();
        var response = await client.SendPersistentOneWayRpcAsync(deviceId, new TbRpcRequest("TestMethod"));
        return new TbEntityId(TbEntityType.RPC, response.RpcId);
    }

    public static Task DeleteRpcAsync(Guid rpcId)
    {
        return TbTestFactory.Instance.CreateRpcClient().DeletePersistentRpcAsync(rpcId);
    }
}
