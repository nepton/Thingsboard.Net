﻿using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbDeviceClient;

[Collection(nameof(TbTestCollection))]
public class GetCustomerDeviceInfosTester
{
    private readonly TbTestFixture _fixture;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public GetCustomerDeviceInfosTester(TbTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task TestGetCustomerDeviceInfos()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var devices = await client.GetCustomerDeviceInfosAsync(_fixture.CustomerId, 20, 0);

        // assert
        Assert.NotNull(devices);
        Assert.NotEmpty(devices.Data);
    }

    [Fact]
    public async Task TestWhenNoDataFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var devices = await client.GetCustomerDeviceInfosAsync(_fixture.CustomerId, 20, 0, textSearch: Guid.NewGuid().ToString());

        // assert
        Assert.NotNull(devices);
        Assert.Empty(devices.Data);
    }

    [Fact]
    public async Task TestWhenCustomerNotFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var devices = await client.GetCustomerDeviceInfosAsync(Guid.NewGuid(), 20, 0, textSearch: Guid.NewGuid().ToString());

        // assert
        Assert.NotNull(devices);
        Assert.Empty(devices.Data);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.GetCustomerDeviceInfosAsync(Guid.NewGuid(), 20, 0, textSearch: Guid.NewGuid().ToString());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.GetCustomerDeviceInfosAsync(Guid.NewGuid(), 20, 0, textSearch: Guid.NewGuid().ToString());
            });
    }
}
