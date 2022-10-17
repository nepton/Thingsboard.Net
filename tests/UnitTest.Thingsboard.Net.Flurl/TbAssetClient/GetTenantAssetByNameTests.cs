using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbAssetClient;

public class GetTenantAssetByNameTests
{
    [Fact]
    public async Task TestGetTenantAssetByName()
    {
        // arrange
        var client   = TbTestFactory.Instance.CreateAssetClient();
        var expected = await AssetUtility.CreateAssetAsync();
        Assert.NotNull(expected);

        // act
        var actual = await client.GetTenantAssetByNameAsync(expected.Name);

        // assert
        Assert.NotNull(actual);
        Assert.Equal(expected.Name, actual.Name);

        // cleanup
        await AssetUtility.DeleteAssetAsync(expected.Id);
    }

    [Fact]
    public async Task TestWhenAssetNotFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAssetClient();

        // act
        var actual = await client.GetTenantAssetByNameAsync("test");

        Assert.Null(actual);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task TestWhenInvalidArgument(string? assetName)
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAssetClient();

        // act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.GetTenantAssetByNameAsync(assetName);
        });
        
        // assert
        Assert.NotNull(ex);
        Assert.IsType<ArgumentException>(ex);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateAssetClient(),
            async client =>
            {
                await client.GetTenantAssetByNameAsync("test");
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateAssetClient(),
            async client =>
            {
                await client.GetTenantAssetByNameAsync("test");
            });
    }
}
