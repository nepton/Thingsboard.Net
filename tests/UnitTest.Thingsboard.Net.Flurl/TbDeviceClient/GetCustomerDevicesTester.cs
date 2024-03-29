﻿using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbDeviceClient;

[Collection(nameof(TbTestCollection))]
public class GetCustomerDevicesTester
{
    private readonly TbTestFixture _fixture;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public GetCustomerDevicesTester(TbTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task TestGetCustomerDevices()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var actual = await client.GetCustomerDevicesAsync(_fixture.CustomerId, 20, 0);

        // assert
        Assert.NotNull(actual);
        Assert.NotEmpty(actual.Data);
    }

    [Fact]
    public async Task TestWhenNoDataFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var actual = await client.GetCustomerDevicesAsync(_fixture.CustomerId, 20, 0, textSearch: Guid.NewGuid().ToString());

        // assert
        Assert.NotNull(actual);
        Assert.Empty(actual.Data);
    }

    [Fact]
    public async Task TestWhenCustomerNotFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var actual = await client.GetCustomerDevicesAsync(Guid.NewGuid(), 20, 0, textSearch: Guid.NewGuid().ToString());

        // assert
        Assert.NotNull(actual);
        Assert.Empty(actual.Data);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.GetCustomerDevicesAsync(Guid.NewGuid(), 20, 0, textSearch: Guid.NewGuid().ToString());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.GetCustomerDevicesAsync(Guid.NewGuid(), 20, 0, textSearch: Guid.NewGuid().ToString());
            });
    }
}
