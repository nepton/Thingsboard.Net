﻿using UnitTest.Thingsboard.Net.Flurl.TbCommon;
using UnitTest.Thingsboard.Net.Flurl.TbDeviceClient;

namespace UnitTest.Thingsboard.Net.Flurl.TbAuditLogClient;

[Collection(nameof(TbTestCollection))]
public class GetAuditLogsTester
{
    private readonly TbTestFixture _fixture;

    public GetAuditLogsTester(TbTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task TestGetAuditLogs()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAuditLogClient();

        // add device for log audit
        var deviceClient = TbTestFactory.Instance.CreateDeviceClient();
        var newDevice    = await deviceClient.SaveDeviceAsync(DeviceUtility.GenerateEntity(_fixture.DeviceProfileId));
        await deviceClient.DeleteDeviceAsync(newDevice.Id.Id);

        // act
        var actual = await client.GetAuditLogsAsync(20, 0);

        // assert
        Assert.NotNull(actual);
        Assert.NotEmpty(actual.Data);
    }

    [Fact]
    public async Task TestWhenNoDataFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAuditLogClient();

        // act
        var actual = await client.GetAuditLogsAsync(20, 0, textSearch: Guid.NewGuid().ToString());

        // assert
        Assert.NotNull(actual);
        Assert.Empty(actual.Data);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateAuditLogClient(),
            async client =>
            {
                await client.GetAuditLogsAsync(20, 0, textSearch: Guid.NewGuid().ToString());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateAuditLogClient(),
            async client =>
            {
                await client.GetAuditLogsAsync(20, 0, textSearch: Guid.NewGuid().ToString());
            });
    }
}
