using Thingsboard.Net.Exceptions;

namespace Thingsboard.Net.Tests.TbTelemetryClient;

public class SaveEntityTelemetryTester
{
    [Fact]
    public async Task TestSaveEntityTelemetry()
    {
        // arrange
        using var service  = new TbTestService();
        var       api      = service.GetRequiredService<ITbTelemetryClient>();
        var       deviceId = service.GetTestDeviceId();

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
