using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbRpcClient;

[Collection(nameof(TbTestCollection))]
public class GetPersistentRpcRequestsTester
{
    private readonly TbTestFixture _fixture;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public GetPersistentRpcRequestsTester(TbTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task TestGetPersistentRpcRequests()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateRpcClient();
        var newRpc = await RpcUtility.SendPersistentOneWayRpcAsync(_fixture.DeviceId);

        // act
        await Task.Delay(1000);
        var actual = await client.GetPersistentRpcRequestsAsync(_fixture.DeviceId, 20, 0);

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
        var actual = await client.GetPersistentRpcRequestsAsync(_fixture.Device2.Id.Id, 20, 0);

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
                await client.GetPersistentRpcRequestsAsync(_fixture.Device2.Id.Id, 20, 0);
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateRpcClient(),
            async client =>
            {
                await client.GetPersistentRpcRequestsAsync(_fixture.Device2.Id.Id, 20, 0);
            });
    }
}
