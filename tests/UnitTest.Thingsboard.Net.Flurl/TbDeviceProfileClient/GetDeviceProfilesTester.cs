﻿using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbDeviceProfileClient;

public class GetDeviceProfilesTester
{
    [Fact]
    public async Task TestGetDeviceProfiles()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceProfileClient();

        // act
        var actual = await client.GetDeviceProfilesAsync(20, 0);

        // assert
        Assert.NotNull(actual);
        Assert.NotEmpty(actual.Data);
    }

    [Fact]
    public async Task TestWhenNoDataFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceProfileClient();

        // act
        var actual = await client.GetDeviceProfilesAsync(20, 0, textSearch: Guid.NewGuid().ToString());

        // assert
        Assert.NotNull(actual);
        Assert.Empty(actual.Data);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateDeviceProfileClient(),
            async client =>
            {
                await client.GetDeviceProfilesAsync(20, 0, textSearch: Guid.NewGuid().ToString());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateDeviceProfileClient(),
            async client =>
            {
                await client.GetDeviceProfilesAsync(20, 0, textSearch: Guid.NewGuid().ToString());
            });
    }
}
