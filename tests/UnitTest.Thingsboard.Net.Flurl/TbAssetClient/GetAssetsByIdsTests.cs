using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbAssetClient;

public class GetAssetsByIdsTests
{
    [Fact]
    public async Task TestGetAssetsByIds()
    {
        // arrange
        var client    = TbTestFactory.Instance.CreateAssetClient();
        var expected1 = await AssetUtility.CreateAssetAsync();
        var expected2 = await AssetUtility.CreateAssetAsync();

        // act
        var actual = await client.GetAssetsByIdsAsync(new[] {expected1.Id.Id, expected2.Id.Id});

        Assert.NotNull(actual);
        Assert.Equal(2, actual.Length);
        Assert.Contains(actual, d => d.Id == expected1.Id);
        Assert.Contains(actual, d => d.Id == expected2.Id);

        // cleanup
        await client.DeleteAssetAsync(expected1.Id.Id);
        await client.DeleteAssetAsync(expected2.Id.Id);
    }

    [Fact]
    public async Task TestWhenAssetNotFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAssetClient();

        // act
        var actual = await client.GetAssetsByIdsAsync(new[] {Guid.Empty, Guid.NewGuid()});

        // assert
        Assert.NotNull(actual);
        Assert.Empty(actual);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateAssetClient(),
            async client =>
            {
                await client.GetAssetsByIdsAsync(new[] {Guid.NewGuid(), Guid.NewGuid()});
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateAssetClient(),
            async client =>
            {
                await client.GetAssetsByIdsAsync(new[] {Guid.NewGuid(), Guid.NewGuid()});
            });
    }
}
