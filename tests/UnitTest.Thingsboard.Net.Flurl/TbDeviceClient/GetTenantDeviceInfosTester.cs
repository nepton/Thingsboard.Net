﻿using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbDeviceClient;

public class GetTenantDeviceInfosTester
{
    [Fact]
    public async Task TestGetTenantDeviceInfos()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var devices = await client.GetTenantDeviceInfosAsync(20, 0);

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
        var devices = await client.GetTenantDeviceInfosAsync(20, 0, textSearch: Guid.NewGuid().ToString());

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
                await client.GetTenantDeviceInfosAsync(20, 0, textSearch: Guid.NewGuid().ToString());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.GetTenantDeviceInfosAsync(20, 0, textSearch: Guid.NewGuid().ToString());
            });
    }
}
