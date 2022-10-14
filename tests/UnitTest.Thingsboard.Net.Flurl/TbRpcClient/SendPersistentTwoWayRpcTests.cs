using Thingsboard.Net;
using Thingsboard.Net.Exceptions;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbRpcClient;

/// <summary>
/// This class is used to test the saveRpc methods of the TbRpcClient class.
/// We will following scenarios:
/// 1. Save new rpc with a valid rpc object.
/// 2. Save new rpc with an invalid rpc object.
/// 3. Save new rpc with a null rpc object.
/// 4. Update an exists rpc with a valid rpc object.
/// 5. Update an not exists rpc with a valid rpc object.
/// </summary>
public class SendPersistentTwoWayRpcTests
{
    /// <summary>
    /// Save new rpc with a valid rpc object.
    /// </summary>
    [Fact]
    public async Task TestSendRpc()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateRpcClient();

        // act
        var newRpc = await client.SendPersistentTwoWayRpcAsync(TbTestData.TestDeviceId, new TbRpcRequest("testMethod"));

        // assert
        Assert.NotNull(newRpc);

        // cleanup
        await client.DeletePersistentRpcAsync(newRpc.RpcId);
    }

    /// <summary>
    /// Save new rpc with an invalid rpc object.
    /// </summary>
    [Fact]
    public async Task TestSendRpcWithNullRequest()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateRpcClient();

        // act
        var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await client.SendPersistentTwoWayRpcAsync(TbTestData.TestDeviceId, null!));

        // assert
        Assert.NotNull(ex);
    }

    /// <summary>
    /// Save new rpc with an invalid rpc object.
    /// </summary>
    [Fact]
    public async Task TestSendRpcWithNonexistentDevice()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateRpcClient();

        // act
        var ex = await Assert.ThrowsAsync<TbEntityNotFoundException>(async () => await client.SendPersistentTwoWayRpcAsync(Guid.NewGuid(), new TbRpcRequest("testMethod")));

        // assert
        Assert.NotNull(ex);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateRpcClient(),
            async client =>
            {
                await client.SendPersistentTwoWayRpcAsync(TbTestData.TestDeviceId, new TbRpcRequest("testMethod"));
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateRpcClient(),
            async client =>
            {
                await client.SendPersistentTwoWayRpcAsync(TbTestData.TestDeviceId, new TbRpcRequest("testMethod"));
            });
    }
}
