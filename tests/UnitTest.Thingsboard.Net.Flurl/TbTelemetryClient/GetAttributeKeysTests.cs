﻿using Thingsboard.Net;
using Thingsboard.Net.Exceptions;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbTelemetryClient;

/// <summary>
/// This class is used to test the GetAttributeKeys method of <see cref="TbAlarmClient"/> class.
/// We will following scenarios:
/// 1. Get all entities with limit.
/// 2. Get nothing has right response.
/// </summary>
[Collection(nameof(TbTestCollection))]
public class GetAttributeKeysTests
{
    private readonly TbTestFixture _fixture;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public GetAttributeKeysTests(TbTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task TestGetAttributeKeys()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();

        // act
        var entities = await client.GetAttributeKeysAsync(TbEntityType.DEVICE, _fixture.DeviceId);

        // assert
        Assert.NotNull(entities);
        Assert.NotEmpty(entities);
        Assert.Contains(entities, x => x == "active");
    }

    [Fact]
    public async Task TestGetAttributeKeysWhenDeviceNotExists()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();

        // act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.GetAttributeKeysAsync(TbEntityType.DEVICE, _fixture.CustomerId);
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
                await client.GetAttributeKeysAsync(TbEntityType.DEVICE, _fixture.DeviceId);
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(
            TbTestFactory.Instance.CreateTelemetryClient(),
            async client =>
            {
                await client.GetAttributeKeysAsync(TbEntityType.DEVICE, _fixture.DeviceId);
            });
    }
}
