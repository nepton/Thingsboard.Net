using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbDeviceClient;

public class GetDeviceCredentialsTests
{
    [Fact]
    public async Task TestGetDeviceCredentials()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var actual   = await client.GetDeviceCredentialsAsync(TbTestData.GetTestDeviceId());
        var expected = "A1_TEST_TOKEN";

        // assert
        Assert.NotNull(actual);
        Assert.Equal(expected, actual!.CredentialsId);
    }

    [Fact]
    public async Task TestWhenDeviceNotFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var device = await client.GetDeviceCredentialsAsync(Guid.Empty);

        // assert
        Assert.Null(device);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.GetDeviceCredentialsAsync(Guid.NewGuid());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.GetDeviceCredentialsAsync(Guid.NewGuid());
            });
    }
}
