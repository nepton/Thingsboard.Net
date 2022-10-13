using Microsoft.Extensions.Options;
using Thingsboard.Net;
using Thingsboard.Net.Flurl;
using Thingsboard.Net.Flurl.DependencyInjection;
using Thingsboard.Net.Flurl.Options;
using Thingsboard.Net.Flurl.Utilities;

namespace UnitTest.Thingsboard.Net.Flurl.DependencyInjection;

public class UnitTest
{
    [Fact]
    public void TestObjectResolve()
    {
        using var services = new TbTestService();

        Assert.IsType<FlurlRequestBuilder>(services.GetRequiredService<IRequestBuilder>());

        Assert.IsType<InMemoryAccessTokenRepository>(services.GetRequiredService<IAccessTokenRepository>());
        Assert.IsType<OptionsSnapshotReader>(services.GetRequiredService<IOptionsReader>());

        Assert.IsType<FlurlTbAlarmClient>(services.GetRequiredService<ITbAlarmClient>());
        Assert.IsType<FlurlTbAuthClient>(services.GetRequiredService<ITbAuthClient>());
        Assert.IsType<FlurlTbDeviceClient>(services.GetRequiredService<ITbDeviceClient>());
        Assert.IsType<FlurlTbEntityQueryClient>(services.GetRequiredService<ITbEntityQueryClient>());
        Assert.IsType<FlurlTbLoginClient>(services.GetRequiredService<ITbLoginClient>());
        Assert.IsType<FlurlTbTelemetryClient>(services.GetRequiredService<ITbTelemetryClient>());
        Assert.IsType<FlurlTbRpcClient>(services.GetRequiredService<ITbRpcClient>());
        Assert.IsType<FlurlTbAssetClient>(services.GetRequiredService<ITbAssetClient>());
        Assert.IsType<FlurlTbCustomerClient>(services.GetRequiredService<ITbCustomerClient>());
        Assert.IsType<FlurlTbDashboardClient>(services.GetRequiredService<ITbDashboardClient>());
        Assert.IsType<FlurlTbAuditLogClient>(services.GetRequiredService<ITbAuditLogClient>());
        Assert.IsType<FlurlTbQueueClient>(services.GetRequiredService<ITbQueueClient>());
    }

    [Fact]
    public void TestOptionsValue()
    {
        using var services = new TbTestService();
        var       expected = services.DefaultOptions;

        var options = services.GetRequiredService<IOptionsSnapshot<ThingsboardNetFlurlOptions>>();

        Assert.StrictEqual(expected, options.Value);
    }
}
