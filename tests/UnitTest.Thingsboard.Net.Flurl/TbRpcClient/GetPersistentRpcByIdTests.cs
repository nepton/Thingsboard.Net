using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbRpcClient;

public class GetPersistentRpcByIdTests
{
    [Fact]
    public async Task TestGetPersistentRpcById()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateRpcClient();
        var newRpc = await RpcUtility.SendPersistentOneWayRpcAsync();

        // act
        var rpcInfo = await client.GetPersistentRpcByIdAsync(newRpc.Id);

        Assert.NotNull(rpcInfo);
        Assert.Equal(newRpc.Id, rpcInfo.Id.Id);

        // cleanup
        await RpcUtility.DeleteRpcAsync(newRpc.Id);
    }

    [Fact]
    public async Task TestWhenRpcNotFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateRpcClient();

        // act
        var rpcId   = Guid.Empty;
        var rpcInfo = await client.GetPersistentRpcByIdAsync(rpcId);

        Assert.Null(rpcInfo);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateRpcClient(),
            async client =>
            {
                await client.GetPersistentRpcByIdAsync(Guid.NewGuid());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateRpcClient(),
            async client =>
            {
                await client.GetPersistentRpcByIdAsync(Guid.NewGuid());
            });
    }
}
