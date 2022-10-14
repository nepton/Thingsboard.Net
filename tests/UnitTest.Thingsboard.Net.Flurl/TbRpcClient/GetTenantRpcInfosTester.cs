using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbRpcClient;

public class GetPersistentRpcRequestsTester
{
    [Fact]
    public async Task TestGetPersistentRpcRequests()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateRpcClient();
        var newRpc = await RpcUtility.SendOneWayRpcAsync();

        // act
        var actual = await client.GetPersistentRpcRequestsAsync(TbTestData.TestDeviceId, 20, 0);

        // assert
        Assert.NotNull(actual);
        Assert.NotEmpty(actual.Data);

        // cleanup
        await RpcUtility.DeleteRpcAsync(newRpc.Id);
    }

    [Fact]
    public async Task TestWhenNoDataFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateRpcClient();

        // act
        var actual = await client.GetPersistentRpcRequestsAsync(TbTestData.TestDeviceId2, 20, 0);

        // assert
        Assert.NotNull(actual);
        Assert.Empty(actual.Data);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateRpcClient(),
            async client =>
            {
                await client.GetPersistentRpcRequestsAsync(TbTestData.TestDeviceId2, 20, 0);
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateRpcClient(),
            async client =>
            {
                await client.GetPersistentRpcRequestsAsync(TbTestData.TestDeviceId2, 20, 0);
            });
    }
}
