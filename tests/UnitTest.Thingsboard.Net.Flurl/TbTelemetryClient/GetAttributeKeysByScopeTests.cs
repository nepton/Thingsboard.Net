using Thingsboard.Net;
using Thingsboard.Net.Exceptions;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbTelemetryClient;

/// <summary>
/// This class is used to test the GetAttributeKeysByScope method of <see cref="TbAlarmClient"/> class.
/// We will following scenarios:
/// 1. Get all entities with limit.
/// 2. Get nothing has right response.
/// </summary>
[Collection(nameof(TbTestCollection))]
public class GetAttributeKeysByScopeTests
{
    private readonly TbTestFixture _fixture;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public GetAttributeKeysByScopeTests(TbTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task TestGetAttributeKeysByScope()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();

        // act
        var entities = await client.GetAttributeKeysByScopeAsync(TbEntityType.DEVICE, _fixture.DeviceId, TbAttributeScope.SERVER_SCOPE);

        // assert
        Assert.NotNull(entities);
        Assert.NotEmpty(entities);
        Assert.Contains(entities, x => x == "active");
    }

    [Fact]
    public async Task TestGetNothing()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();

        // act
        var entities = await client.GetAttributeKeysByScopeAsync(TbEntityType.DEVICE, _fixture.DeviceId, TbAttributeScope.CLIENT_SCOPE);

        // assert
        Assert.NotNull(entities);
        Assert.Empty(entities);
    }

    [Fact]
    public async Task TestGetAttributeKeysByScopeWhenDeviceNotExists()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();

        // act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.GetAttributeKeysByScopeAsync(TbEntityType.DEVICE, _fixture.CustomerId, TbAttributeScope.CLIENT_SCOPE);
        });

        // assert
        Assert.NotNull(ex);
        Assert.IsType<TbEntityNotFoundException>(ex);
        Assert.Equal(new TbEntityId(TbEntityType.DEVICE, _fixture.CustomerId), ((TbEntityNotFoundException) ex).EntityId);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(
            TbTestFactory.Instance.CreateTelemetryClient(),
            async client =>
            {
                await client.GetAttributeKeysByScopeAsync(TbEntityType.DEVICE, _fixture.DeviceId, TbAttributeScope.CLIENT_SCOPE);
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(
            TbTestFactory.Instance.CreateTelemetryClient(),
            async client =>
            {
                await client.GetAttributeKeysByScopeAsync(TbEntityType.DEVICE, _fixture.DeviceId, TbAttributeScope.CLIENT_SCOPE);
            });
    }
}
