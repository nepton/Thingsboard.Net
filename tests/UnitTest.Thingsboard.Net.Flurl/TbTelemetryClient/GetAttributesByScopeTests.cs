using Thingsboard.Net;
using Thingsboard.Net.Exceptions;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbTelemetryClient;

/// <summary>
/// This class is used to test the GetAttributesByScope method of <see cref="TbAlarmClient"/> class.
/// We will following scenarios:
/// 1. Get all entities with limit.
/// 2. Get nothing has right response.
/// </summary>
public class GetAttributesByScopeTests
{
    [Fact]
    public async Task TestGetAttributesByScope()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();

        // act
        var entities = await client.GetAttributesByScopeAsync(TbEntityType.DEVICE, TbTestData.GetTestDeviceId(), TbAttributeScope.SERVER_SCOPE, new[] {"active"});

        // assert
        Assert.NotNull(entities);
        Assert.NotEmpty(entities);
        Assert.Contains(entities, x => x.Key == "active");
    }

    [Fact]
    public async Task TestGetAttributesByScopeThatDoesNotExists()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();

        // act
        var entities = await client.GetAttributesByScopeAsync(TbEntityType.DEVICE, TbTestData.GetTestDeviceId(), TbAttributeScope.SERVER_SCOPE, new[] {"test_not_exists"});

        // assert
        Assert.NotNull(entities);
        Assert.Empty(entities);
    }

    [Fact]
    public async Task TestGetAttributesByScopeWhenDeviceNotExists()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();

        // act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.GetAttributesByScopeAsync(TbEntityType.DEVICE, TbTestData.GetTestCustomerId(), TbAttributeScope.SERVER_SCOPE, new[] {"active"});
        });

        // assert
        Assert.NotNull(ex);
        Assert.IsType<TbEntityNotFoundException>(ex);
        Assert.Equal(new TbEntityId(TbEntityType.DEVICE, TbTestData.GetTestCustomerId()), ((TbEntityNotFoundException) ex).EntityId);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(
            TbTestFactory.Instance.CreateTelemetryClient(),
            async client =>
            {
                await client.GetAttributesByScopeAsync(TbEntityType.DEVICE, TbTestData.GetTestDeviceId(), TbAttributeScope.SERVER_SCOPE, new[] {"active"});
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(
            TbTestFactory.Instance.CreateTelemetryClient(),
            async client =>
            {
                await client.GetAttributesByScopeAsync(TbEntityType.DEVICE, TbTestData.GetTestDeviceId(), TbAttributeScope.SERVER_SCOPE, new[] {"active"});
            });
    }
}
