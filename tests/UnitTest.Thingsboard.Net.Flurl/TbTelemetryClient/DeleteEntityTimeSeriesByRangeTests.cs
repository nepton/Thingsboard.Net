﻿using Thingsboard.Net;
using Thingsboard.Net.Exceptions;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbTelemetryClient;

/// <summary>
/// This class is used to test the deleteEntityTimeSeries method of the TbEntityTimeSeriesClient class.
/// 
/// We test following scenarios:
/// 1: Delete an alarm that exists.
/// 2: Delete an alarm that does not exist.
/// </summary>
[Collection(nameof(TbTestCollection))]
public class DeleteEntityTimeSeriesByRangeTests
{
    private readonly TbTestFixture _fixture;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public DeleteEntityTimeSeriesByRangeTests(TbTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task TestDeleteExistsEntityTimeSeries()
    {
        // Arrange
        var client  = TbTestFactory.Instance.CreateTelemetryClient();
        var testKey = "testDeleteExistsEntityTimeSeries";
        await client.SaveEntityTimeSeriesAsync(TbEntityType.DEVICE, _fixture.DeviceId, new Dictionary<string, object> {{testKey, "testValue"}});

        // Act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.DeleteEntityTimeSeriesAsync(TbEntityType.DEVICE, _fixture.DeviceId, new[] {testKey}, DateTime.Today.AddDays(-7), DateTime.Now);
        });

        // Assert
        Assert.Null(ex);
    }

    [Fact]
    public async Task TestDeleteEntityTimeSeriesWhileEntityNotExist()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();

        // Act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.DeleteEntityTimeSeriesAsync(TbEntityType.DEVICE,
                _fixture.CustomerId,
                new[] {$"test_not_exist{new Random().Next()}"},
                DateTime.Today.AddDays(-7),
                DateTime.Now);
        });

        // Assert
        Assert.IsType<TbEntityNotFoundException>(ex);
        Assert.Equal(new TbEntityId(TbEntityType.DEVICE, _fixture.CustomerId), ((TbEntityNotFoundException) ex).EntityId);
    }

    [Fact]
    public async Task TestDeleteEntityTimeSeriesWhenKeyNotExists()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();

        // Act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.DeleteEntityTimeSeriesAsync(TbEntityType.DEVICE,
                _fixture.DeviceId,
                new[] {$"test_not_exist{new Random().Next()}"},
                DateTime.Today.AddDays(-7),
                DateTime.Now);
        });

        // Assert
        Assert.Null(ex);
    }

    [Fact]
    public async Task TestDeleteEmptyEntityTimeSeries()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();

        // Act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.DeleteEntityTimeSeriesAsync(TbEntityType.DEVICE, _fixture.DeviceId, Array.Empty<string>(), DateTime.Today.AddDays(-7), DateTime.Now);
        });

        // Assert
        Assert.IsType<ArgumentException>(ex);
    }

    [Fact]
    public async Task TestDeleteNullEntityTimeSeries()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();

        // Act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.DeleteEntityTimeSeriesAsync(TbEntityType.DEVICE, _fixture.DeviceId, null!, DateTime.Today.AddDays(-7), DateTime.Now);
        });

        // Assert
        Assert.IsType<ArgumentNullException>(ex);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(
            TbTestFactory.Instance.CreateTelemetryClient(),
            async client =>
            {
                await client.DeleteEntityTimeSeriesAsync(TbEntityType.DEVICE, Guid.NewGuid(), new[] {"testId123"}, DateTime.Today.AddDays(-7), DateTime.Now);
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(
            TbTestFactory.Instance.CreateTelemetryClient(),
            async client =>
            {
                await client.DeleteEntityTimeSeriesAsync(TbEntityType.DEVICE, Guid.NewGuid(), new[] {"testId123"}, DateTime.Today.AddDays(-7), DateTime.Now);
            });
    }
}
