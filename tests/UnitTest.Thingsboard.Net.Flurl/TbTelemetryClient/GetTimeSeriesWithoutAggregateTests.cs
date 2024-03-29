﻿using Thingsboard.Net;
using Thingsboard.Net.Exceptions;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbTelemetryClient;

/// <summary>
/// This class is used to test the GetTimeSeries method of <see cref="TbAlarmClient"/> class.
/// We will following scenarios:
/// 1. Get all entities with limit.
/// 2. Get nothing has right response.
/// </summary>
[Collection(nameof(TbTestCollection))]
public class GetTimeSeriesWithoutAggregateTests
{
    private readonly TbTestFixture _fixture;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public GetTimeSeriesWithoutAggregateTests(TbTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task TestGetTimeSeries()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();
        var field1 = "temperature";
        var field2 = "humidity";
        await client.SaveEntityTimeSeriesAsync(TbEntityType.DEVICE, _fixture.DeviceId, new Dictionary<string, object> {{field1, 10}, {field2, 20}});

        // act
        var entities = await client.GetTimeSeriesAsync(TbEntityType.DEVICE, _fixture.DeviceId, new[] {field1, field2}, DateTime.Today.AddDays(-7), DateTime.Now);

        // assert
        Assert.NotNull(entities);
        Assert.NotEmpty(entities);
        Assert.Contains(entities, x => x.Key.Key == field1);
        Assert.Contains(entities, x => x.Key.Key == field2);

        // cleanup
        await client.DeleteEntityTimeSeriesAsync(TbEntityType.DEVICE, _fixture.DeviceId, new[] {field1, field2});
    }

    [Fact]
    public async Task TestGetTimeSeriesThatDoesNotExists()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();

        // act
        var entities = await client.GetTimeSeriesAsync(TbEntityType.DEVICE, _fixture.DeviceId, new[] {"test_not_exists"}, DateTime.Today.AddDays(-7), DateTime.Now);

        // assert
        Assert.NotNull(entities);
        Assert.Empty(entities);
    }

    [Fact]
    public async Task TestGetTimeSeriesWhenDeviceNotExists()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();

        // act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.GetTimeSeriesAsync(TbEntityType.DEVICE, _fixture.CustomerId, new[] {"active"}, DateTime.Today.AddDays(-7), DateTime.Now);
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
                await client.GetTimeSeriesAsync(TbEntityType.DEVICE, _fixture.DeviceId, new[] {"active"}, DateTime.Today.AddDays(-7), DateTime.Now);
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(
            TbTestFactory.Instance.CreateTelemetryClient(),
            async client =>
            {
                await client.GetTimeSeriesAsync(TbEntityType.DEVICE, _fixture.DeviceId, new[] {"active"}, DateTime.Today.AddDays(-7), DateTime.Now);
            });
    }
}
