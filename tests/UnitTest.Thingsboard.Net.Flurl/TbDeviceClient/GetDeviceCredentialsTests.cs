using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbDeviceClient;

[Collection(nameof(TbTestCollection))]
public class GetDeviceCredentialsTests
{
    private readonly TbTestFixture _fixture;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public GetDeviceCredentialsTests(TbTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task TestGetDeviceCredentials()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var actual   = await client.GetDeviceCredentialsAsync(_fixture.DeviceId);
        var expected = "A1_TEST_TOKEN";

        // assert
        Assert.NotNull(actual);
        Assert.Equal(expected, actual.CredentialsId);
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
