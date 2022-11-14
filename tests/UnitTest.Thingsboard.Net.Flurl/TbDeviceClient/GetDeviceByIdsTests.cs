using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbDeviceClient;

[Collection(nameof(TbTestCollection))]
public class GetDevicesByIdsTests
{
    private readonly TbTestFixture _fixture;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public GetDevicesByIdsTests(TbTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task TestGetDevicesByIds()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var actual = await client.GetDevicesByIdsAsync(new[] {_fixture.DeviceId, _fixture.Device2.Id.Id});

        Assert.NotNull(actual);
        Assert.Equal(2, actual.Length);
        Assert.Contains(actual, d => d.Id.Id == _fixture.DeviceId);
        Assert.Contains(actual, d => d.Id.Id == _fixture.Device2.Id.Id);
    }

    [Fact]
    public async Task TestWhenDeviceNotFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var actual = await client.GetDevicesByIdsAsync(new[] {Guid.Empty, Guid.NewGuid()});

        // assert
        Assert.NotNull(actual);
        Assert.Empty(actual);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.GetDevicesByIdsAsync(new[] {_fixture.DeviceId, _fixture.Device2.Id.Id});
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.GetDevicesByIdsAsync(new[] {_fixture.DeviceId, _fixture.Device2.Id.Id});
            });
    }
}
