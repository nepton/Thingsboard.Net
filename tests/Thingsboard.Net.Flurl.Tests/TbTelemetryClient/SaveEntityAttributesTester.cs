using Thingsboard.Net.Exceptions;

namespace Thingsboard.Net.Tests.TbTelemetryClient;

public class SaveEntityAttributesTester
{
    [Fact]
    public async Task TestSaveEntityAttributes()
    {
        // arrange
        using var service  = new TbTestService();
        var       api      = service.GetRequiredService<ITbTelemetryClient>();
        var       deviceId = service.GetTestDeviceId();

        // act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await api.SaveEntityAttributesAsync(TbEntityType.DEVICE, deviceId, TbAttributeScope.CLIENT_SCOPE, new Dictionary<string, string> {{"key", "value"}});
        });

        // act
        Assert.IsType<TbHttpException>(ex);
    }
}
