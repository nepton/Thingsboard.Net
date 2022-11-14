using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbAssetClient;

[Collection(nameof(TbTestCollection))]
public class GetCustomerAssetsTester
{
    private readonly TbTestFixture _fixture;

    public GetCustomerAssetsTester(TbTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task TestGetCustomerAssets()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAssetClient();

        // act
        var actual = await client.GetCustomerAssetAsync(_fixture.Customer.Id.Id, 20, 0);

        // assert
        Assert.NotNull(actual);
    }

    [Fact]
    public async Task TestWhenNoDataFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAssetClient();

        // act
        var actual = await client.GetCustomerAssetAsync(_fixture.Customer.Id.Id, 20, 0, textSearch: Guid.NewGuid().ToString());

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
        var actual = await client.GetCustomerAssetAsync(Guid.NewGuid(), 20, 0, textSearch: Guid.NewGuid().ToString());

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
                await client.GetCustomerAssetAsync(Guid.NewGuid(), 20, 0, textSearch: Guid.NewGuid().ToString());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateAssetClient(),
            async client =>
            {
                await client.GetCustomerAssetAsync(Guid.NewGuid(), 20, 0, textSearch: Guid.NewGuid().ToString());
            });
    }
}
