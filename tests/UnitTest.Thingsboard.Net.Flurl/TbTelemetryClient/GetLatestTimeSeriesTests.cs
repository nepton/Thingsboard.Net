using Thingsboard.Net;
using Thingsboard.Net.Exceptions;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbTelemetryClient;

/// <summary>
/// This class is used to test the GetLatestTimeSeries method of <see cref="TbAlarmClient"/> class.
/// We will following scenarios:
/// 1. Get all entities with limit.
/// 2. Get nothing has right response.
/// </summary>
public class GetLatestTimeSeriesTests
{
    [Fact]
    public async Task TestGetLatestTimeSeries()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();
        var field1 = "temperature";
        var field2 = "humidity";
        await client.SaveEntityTimeSeriesAsync(TbEntityType.DEVICE, TbTestData.TestDeviceId, new Dictionary<string, object> {{field1, 10}, {field2, 20}});

        // act
        var entities = await client.GetLatestTimeSeriesAsync(TbEntityType.DEVICE,
            TbTestData.TestDeviceId,
            new[] {field1, field2}
        );

        // assert
        Assert.NotNull(entities);
        Assert.NotEmpty(entities);
        Assert.Contains(entities, x => x.Key.Key == field1);
        Assert.Contains(entities, x => x.Key.Key == field2);

        // cleanup
        await client.DeleteEntityTimeSeriesAsync(TbEntityType.DEVICE, TbTestData.TestDeviceId, new[] {field1, field2});
    }

    [Fact]
    public async Task TestGetLatestTimeSeriesThatDoesNotExists()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();

        // act
        var entities = await client.GetLatestTimeSeriesAsync(TbEntityType.DEVICE,
            TbTestData.TestDeviceId,
            new[] {"test_not_exists2"}
        );

        // assert
        Assert.NotNull(entities);
        // Assert.Empty(entities);  thingsboard will return the element in list with null value
    }

    [Fact]
    public async Task TestGetLatestTimeSeriesWhenDeviceNotExists()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();

        // act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.GetLatestTimeSeriesAsync(TbEntityType.DEVICE, TbTestData.TestCustomerId, new[] {"active"});
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
                await client.GetLatestTimeSeriesAsync(TbEntityType.DEVICE, TbTestData.TestDeviceId, new[] {"active"});
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(
            TbTestFactory.Instance.CreateTelemetryClient(),
            async client =>
            {
                await client.GetLatestTimeSeriesAsync(TbEntityType.DEVICE, TbTestData.TestDeviceId, new[] {"active"});
            });
    }
}
