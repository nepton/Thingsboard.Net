using Thingsboard.Net;
using Thingsboard.Net.Exceptions;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbTelemetryClient;

/// <summary>
/// This class is used to test the GetTimeSeriesKeys method of <see cref="TbAlarmClient"/> class.
/// We will following scenarios:
/// 1. Get all entities with limit.
/// 2. Get nothing has right response.
/// </summary>
public class GetTimeSeriesKeysTests
{
    [Fact]
    public async Task TestGetTimeSeriesKeys()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();
        var key    = "testGetTimeSeriesKeys"; // WARN: the first letter must be lowercase
        await client.SaveEntityTimeSeriesWithTtlAsync(TbEntityType.DEVICE, TbTestData.TestDeviceId, 1000, new Dictionary<string, object>() {[key] = 100});

        // act
        var entities = await client.GetTimeSeriesKeysAsync(TbEntityType.DEVICE, TbTestData.TestDeviceId);

        // assert
        Assert.NotNull(entities);
        Assert.NotEmpty(entities);
        Assert.Contains(entities, x => x == key);

        // clean up
        await client.DeleteEntityTimeSeriesAsync(TbEntityType.DEVICE, TbTestData.TestDeviceId, new[] {key}, true);
    }

    [Fact]
    public async Task TestGetTimeSeriesKeysWhenDeviceNotExists()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();

        // act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.GetTimeSeriesKeysAsync(TbEntityType.DEVICE, TbTestData.TestCustomerId);
        });

        // assert
        Assert.NotNull(ex);
        Assert.IsType<TbEntityNotFoundException>(ex);
        Assert.Equal(new TbEntityId(TbEntityType.DEVICE, TbTestData.TestCustomerId), ((TbEntityNotFoundException) ex).EntityId);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(
            TbTestFactory.Instance.CreateTelemetryClient(),
            async client =>
            {
                await client.GetTimeSeriesKeysAsync(TbEntityType.DEVICE, TbTestData.TestDeviceId);
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(
            TbTestFactory.Instance.CreateTelemetryClient(),
            async client =>
            {
                await client.GetTimeSeriesKeysAsync(TbEntityType.DEVICE, TbTestData.TestDeviceId);
            });
    }
}
