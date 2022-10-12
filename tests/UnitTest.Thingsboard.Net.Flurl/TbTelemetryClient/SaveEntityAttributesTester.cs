using Thingsboard.Net;
using Thingsboard.Net.Exceptions;

namespace UnitTest.Thingsboard.Net.Flurl.TbTelemetryClient;

public class SaveEntityAttributesTester
{
    [Fact]
    public async Task TestSaveEntityAttributes()
    {
        // arrange
        var api      = TbTestFactory.Instance.CreateTelemetryClient();
        var deviceId = TbTestData.TestDeviceId;

        // act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await api.SaveEntityAttributesAsync(TbEntityType.DEVICE, deviceId, TbAttributeScope.CLIENT_SCOPE, new Dictionary<string, string> {{"key", "value"}});
        });

        // act
        Assert.IsType<TbHttpException>(ex);
    }
}
