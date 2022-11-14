using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbAssetClient;

public class GetAssetByIdTests
{
    [Fact]
    public async Task TestGetAssetById()
    {
        // arrange
        var client   = TbTestFactory.Instance.CreateAssetClient();
        var expected = await AssetUtility.CreateAssetAsync();

        // act
        var actual = await client.GetAssetByIdAsync(expected.Id.Id);

        Assert.NotNull(actual);
        Assert.Equal(expected.Id.Id, actual.Id.Id);

        // cleanup
        await client.DeleteAssetAsync(expected.Id.Id);
    }

    [Fact]
    public async Task TestWhenAssetNotFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAssetClient();

        // act
        var actual = await client.GetAssetByIdAsync(Guid.NewGuid());

        Assert.Null(actual);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateAssetClient(),
            async client =>
            {
                await client.GetAssetByIdAsync(Guid.NewGuid());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateAssetClient(),
            async client =>
            {
                await client.GetAssetByIdAsync(Guid.NewGuid());
            });
    }
}
