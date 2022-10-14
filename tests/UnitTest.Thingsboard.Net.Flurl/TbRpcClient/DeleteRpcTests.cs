using Thingsboard.Net.Exceptions;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbRpcClient;

/// <summary>
/// This class is used to test the deleteRpc method of the TbRpcClient class.
/// 
/// We test following scenarios:
/// 1: Delete an rpc that exists.
/// 2: Delete an rpc that does not exist.
/// </summary>
public class DeleteRpcTests
{
    [Fact]
    public async Task DeleteExistsRpcTest()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateRpcClient();
        var newRpc = await RpcUtility.SendOneWayRpcAsync();

        // Act
        var entityBeforeDelete = await client.GetPersistentRpcByIdAsync(newRpc.Id);
        var exception = await Record.ExceptionAsync(async () =>
        {
            await client.DeletePersistentRpcAsync(newRpc.Id);
        });
        var entityAfterDelete = await client.GetPersistentRpcByIdAsync(newRpc.Id);

        // Assert
        Assert.NotNull(entityBeforeDelete);
        Assert.Null(entityAfterDelete);
        Assert.Null(exception);
    }

    [Fact]
    public async Task DeleteNotExistsRpcTest()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateRpcClient();

        // Act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.DeletePersistentRpcAsync(Guid.NewGuid());
        });

        // Assert
        Assert.IsType<TbEntityNotFoundException>(ex);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateRpcClient(),
            async client =>
            {
                await client.DeletePersistentRpcAsync(Guid.NewGuid());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateRpcClient(),
            async client =>
            {
                await client.DeletePersistentRpcAsync(Guid.NewGuid());
            });
    }
}
