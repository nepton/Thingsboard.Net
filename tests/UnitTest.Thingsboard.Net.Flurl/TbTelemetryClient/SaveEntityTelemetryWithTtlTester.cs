﻿using Thingsboard.Net;
using Thingsboard.Net.Exceptions;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbTelemetryClient;

/// <summary>
/// This class is used to test the saveTelemetry methods of the TbTelemetryClient class.
/// We will following scenarios:
/// 1. Save new alarm with a valid alarm object.
/// 2. Save new alarm with an invalid alarm object.
/// 3. Save new alarm with a null alarm object.
/// 4. Update an exists alarm with a valid alarm object.
/// 5. Update an not exists alarm with a valid alarm object.
/// </summary>
[Collection(nameof(TbTestCollection))]
public class SaveEntityTelemetryWithTtlTester
{
    private readonly TbTestFixture _fixture;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public SaveEntityTelemetryWithTtlTester(TbTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task TestSaveEntityTelemetryWithTtlByDict()
    {
        // arrange
        var client   = TbTestFactory.Instance.CreateTelemetryClient();
        var deviceId = _fixture.DeviceId;
        var upperKey = "UpperKey"; // check UpperKey
        var lowerKey = "lowerKey";

        await client.DeleteEntityTimeSeriesAsync(TbEntityType.DEVICE, _fixture.DeviceId, new[] {upperKey, lowerKey});

        // act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.SaveEntityTimeSeriesWithTtlAsync(TbEntityType.DEVICE,
                deviceId,
                1000,
                new Dictionary<string, string>
                {
                    [upperKey] = "value",
                    [lowerKey] = "value"
                });
        });

        // act
        Assert.Null(ex);
        var keys = await client.GetTimeSeriesKeysAsync(TbEntityType.DEVICE, _fixture.DeviceId);
        Assert.Contains(upperKey, keys);
        Assert.Contains(lowerKey, keys);
    }

    [Fact]
    public async Task TestSaveEntityTelemetryWithTtlByObj()
    {
        // arrange
        var client   = TbTestFactory.Instance.CreateTelemetryClient();
        var deviceId = _fixture.DeviceId;
        var upperKey = "UpperKey"; // check UpperKey
        var lowerKey = "lowerKey";

        await client.DeleteEntityTimeSeriesAsync(TbEntityType.DEVICE, _fixture.DeviceId, new[] {upperKey, lowerKey});

        // act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.SaveEntityTimeSeriesWithTtlAsync(TbEntityType.DEVICE,
                deviceId,
                1000,
                new
                {
                    UpperKey = "value",
                    lowerKey = "value"
                });
        });

        // act
        Assert.Null(ex);
        var keys = await client.GetTimeSeriesKeysAsync(TbEntityType.DEVICE, _fixture.DeviceId);
        Assert.Contains(upperKey, keys);
        Assert.Contains(lowerKey, keys);
    }

    /// <summary>
    /// Save new alarm with an invalid alarm object.
    /// </summary>
    [Fact]
    public async Task TestSaveInvalidTelemetry()
    {
        // arrange
        var client   = TbTestFactory.Instance.CreateTelemetryClient();
        var deviceId = _fixture.DeviceId;

        // act
        var ex = await Record.ExceptionAsync(async () =>
        {
            // this client can't save the data to CLIENT_SCOPE
            await client.SaveEntityTimeSeriesWithTtlAsync(TbEntityType.ALARM, deviceId, -1000, new Dictionary<string, string> {{"key", "value"}});
        });

        // act
        Assert.IsType<TbHttpException>(ex);
    }

    /// <summary>
    /// Save new alarm with an invalid alarm object.
    /// </summary>
    [Fact]
    public async Task TestSaveWhenDeviceIsNull()
    {
        // arrange
        var client   = TbTestFactory.Instance.CreateTelemetryClient();
        var entityId = Guid.NewGuid();

        // act
        var ex = await Record.ExceptionAsync(async () =>
        {
            // this client can't save the data to CLIENT_SCOPE
            await client.SaveEntityTimeSeriesWithTtlAsync(TbEntityType.DEVICE, entityId, 1000, new Dictionary<string, string> {{"key", "value"}});
        });

        // assert
        Assert.IsType<TbEntityNotFoundException>(ex);
        Assert.Equal(new TbEntityId(TbEntityType.DEVICE, entityId), ((TbEntityNotFoundException) ex).EntityId);
    }

    /// <summary>
    /// Save new alarm with an invalid alarm object.
    /// </summary>
    [Fact]
    public async Task TestSaveNullTelemetry()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();

        // act
        var ex = await Record.ExceptionAsync(async () =>
        {
            // this client can't save the data to CLIENT_SCOPE
            await client.SaveEntityTimeSeriesWithTtlAsync(TbEntityType.DEVICE, _fixture.DeviceId, 1000, null!);
        });

        // assert
        Assert.NotNull(ex);
        Assert.IsType<ArgumentNullException>(ex);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(
            TbTestFactory.Instance.CreateTelemetryClient(),
            async client =>
            {
                await client.SaveEntityTimeSeriesWithTtlAsync(TbEntityType.DEVICE,
                    _fixture.DeviceId,
                    1000,
                    new Dictionary<string, string> {{"key", "value"}});
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(
            TbTestFactory.Instance.CreateTelemetryClient(),
            async client =>
            {
                await client.SaveEntityTimeSeriesWithTtlAsync(TbEntityType.DEVICE,
                    _fixture.DeviceId,
                    1000,
                    new Dictionary<string, string> {{"key", "value"}});
            });
    }
}
