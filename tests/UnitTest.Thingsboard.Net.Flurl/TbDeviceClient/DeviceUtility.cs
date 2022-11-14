using Thingsboard.Net;

namespace UnitTest.Thingsboard.Net.Flurl.TbDeviceClient;

public class DeviceUtility
{
    public static TbNewDevice GenerateEntity(Guid deviceProfileId)
    {
        var device = new TbNewDevice
        {
            Name            = Guid.NewGuid().ToString(),
            Type            = "Test Device Type",
            Label           = "Test Device Label",
            DeviceProfileId = new TbEntityId(TbEntityType.DEVICE_PROFILE, deviceProfileId),
        };

        return device;
    }

    public static async Task<TbDevice> CreateDeviceAsync(Guid deviceProfileId)
    {
        var client = TbTestFactory.Instance.CreateDeviceClient();
        var device = GenerateEntity(deviceProfileId);
        return await client.SaveDeviceAsync(device);
    }
}
