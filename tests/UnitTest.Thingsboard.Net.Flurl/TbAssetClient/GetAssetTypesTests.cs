using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbAssetClient;

/// <summary>
/// This class is used to test the GetAssetTypes method of <see cref="TbAlarmClient"/> class.
/// </summary>
public class GetAssetTypesTests
{
    [Fact]
    public async Task TestGetAssetTypes()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAssetClient();

        // act
        var actual = await client.GetAssetTypesAsync();

        // assert
        Assert.NotNull(actual);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(
            TbTestFactory.Instance.CreateAssetClient(),
            async client =>
            {
                await client.GetAssetTypesAsync();
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(
            TbTestFactory.Instance.CreateAssetClient(),
            async client =>
            {
                await client.GetAssetTypesAsync();
            });
    }
}
