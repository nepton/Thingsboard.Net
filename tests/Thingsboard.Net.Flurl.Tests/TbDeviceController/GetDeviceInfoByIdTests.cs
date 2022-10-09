﻿using Newtonsoft.Json;
using Quibble.CSharp;
using Quibble.Xunit;
using Thingsboard.Net.TbDeviceController;

namespace Thingsboard.Net.Tests.TbDeviceController;

public class GetDeviceInfoByIdTests
{
    [Fact]
    public async Task TestGetDeviceInfoById()
    {
        // arrange
        using var service = new TbTestService();
        var       api     = service.GetRequiredService<ITbDeviceApi>();

        // act
        var deviceId   = Guid.Parse("ab5371c0-47a2-11ed-8248-233ce934eba0");
        var deviceInfo = await api.GetDeviceInfoByIdAsync(deviceId);

        Assert.NotNull(deviceInfo);

        var json = JsonConvert.SerializeObject(deviceInfo);
        var expected =
            """{"Id":{"Id":"ab5371c0-47a2-11ed-8248-233ce934eba0","EntityType":6},"CreatedTime":1665299950300,"TenantId":{"Id":"aaf39e80-47a2-11ed-8248-233ce934eba0","EntityType":15},"CustomerId":{"Id":"ab23af30-47a2-11ed-8248-233ce934eba0","EntityType":4},"Name":"Test Device A1","Type":"default","Label":null,"DeviceProfileId":{"Id":"aaf7e440-47a2-11ed-8248-233ce934eba0","EntityType":7},"FirmwareId":null,"SoftwareId":null,"CustomerTitle":"Customer A","CustomerIsPublic":false,"DeviceProfileName":"default"}""";
        JsonAssert.Equal(expected, json);
    }

    [Fact]
    public async Task TestWhenDeviceNotFound()
    {
        // arrange
        using var service = new TbTestService();
        var       api     = service.GetRequiredService<ITbDeviceApi>();

        // act
        var deviceId   = Guid.Empty;
        var deviceInfo = await api.GetDeviceInfoByIdAsync(deviceId);

        Assert.Null(deviceInfo);
    }
}