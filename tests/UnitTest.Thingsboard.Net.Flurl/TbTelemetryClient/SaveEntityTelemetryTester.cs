using Thingsboard.Net;

namespace UnitTest.Thingsboard.Net.Flurl.TbTelemetryClient;

public class SaveEntityTelemetryTester
{
    [Fact]
    public async Task TestSaveEntityTelemetry()
    {
        // arrange
        var api      = TbTestFactory.Instance.CreateTelemetryClient();
        var deviceId = TbTestData.TestDeviceId;

        // act
        await api.SaveEntityTelemetryAsync(TbEntityType.DEVICE, deviceId, new {temperature = 42});
        // var ex = await Record.ExceptionAsync(async () =>
        // {
        //     await api.SaveEntityTelemetryAsync(TbEntityType.DEVICE, deviceId, new {temperature = 42});
        // });

        // act
        // Assert.Null(ex);
    }
}
