using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbAssetClient;

[Collection(nameof(TbTestCollection))]
public class GetCustomerAssetInfosTester
{
    private readonly TbTestFixture _fixture;

    public GetCustomerAssetInfosTester(TbTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task TestGetCustomerAssetInfos()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAssetClient();
        var asset  = await AssetUtility.CreateAssetAsync();
        await client.AssignAssetToCustomerAsync(_fixture.CustomerId, asset.Id.Id);

        // act
        var actual = await client.GetCustomerAssetInfosAsync(_fixture.CustomerId, 20, 0);

        // assert
        Assert.NotNull(actual);
        Assert.NotEmpty(actual.Data);

        // cleanup
        await AssetUtility.DeleteAssetAsync(asset.Id);
    }

    [Fact]
    public async Task TestWhenNoDataFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAssetClient();

        // act
        var actual = await client.GetCustomerAssetInfosAsync(_fixture.CustomerId, 20, 0, textSearch: Guid.NewGuid().ToString());

        // assert
        Assert.NotNull(actual);
        Assert.Empty(actual.Data);
    }

    [Fact]
    public async Task TestWhenCustomerNotFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAssetClient();

        // act
        var actual = await client.GetCustomerAssetInfosAsync(Guid.NewGuid(), 20, 0, textSearch: Guid.NewGuid().ToString());

        // assert
        Assert.NotNull(actual);
        Assert.Empty(actual.Data);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateAssetClient(),
            async client =>
            {
                await client.GetCustomerAssetInfosAsync(Guid.NewGuid(), 20, 0, textSearch: Guid.NewGuid().ToString());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateAssetClient(),
            async client =>
            {
                await client.GetCustomerAssetInfosAsync(Guid.NewGuid(), 20, 0, textSearch: Guid.NewGuid().ToString());
            });
    }
}
