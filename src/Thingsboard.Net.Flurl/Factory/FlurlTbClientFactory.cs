using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Thingsboard.Net.Flurl.Options;
using Thingsboard.Net.Flurl.Utilities;

namespace Thingsboard.Net.Flurl;

/// <summary>
/// The factory for creating a <see cref="ITbClient{TClient}"/> instance.
/// </summary>
public class FlurlTbClientFactory
{
    public static FlurlTbClientFactory Instance { get; } = new();

    public ThingsboardNetFlurlOptions Options { get; set; } = new();

    public ILoggerFactory? LoggerFactory { get; set; }

    public IRequestBuilder CreateRequestBuilder()
    {
        return new FlurlRequestBuilder(
            new DefaultOptionsReaderFactory(new ThingsboardNetFlurlOptionsReader(Options)),
            new InMemoryAccessTokenRepository(),
            LoggerFactory ?? NullLoggerFactory.Instance);
    }

    public ITbAlarmClient CreateAlarmClient() => new FlurlTbAlarmClient(CreateRequestBuilder());

    public ITbAssetClient CreateAssetClient() => new FlurlTbAssetClient(CreateRequestBuilder());

    public ITbAuditLogClient CreateAuditLogClient() => new FlurlTbAuditLogClient(CreateRequestBuilder());

    public ITbCustomerClient CreateCustomerClient() => new FlurlTbCustomerClient(CreateRequestBuilder());

    public ITbDashboardClient CreateDashboardClient() => new FlurlTbDashboardClient(CreateRequestBuilder());

    public ITbDeviceClient CreateDeviceClient() => new FlurlTbDeviceClient(CreateRequestBuilder());

    public ITbEntityQueryClient CreateEntityQueryClient() => new FlurlTbEntityQueryClient(CreateRequestBuilder());

    public ITbLoginClient CreateLoginClient() => new FlurlTbLoginClient(CreateRequestBuilder());

    public ITbQueueClient CreateQueueClient() => new FlurlTbQueueClient(CreateRequestBuilder());

    public ITbRpcClient CreateRpcClient() => new FlurlTbRpcClient(CreateRequestBuilder());

    public ITbTelemetryClient CreateTelemetryClient() => new FlurlTbTelemetryClient(CreateRequestBuilder());

    public ITbAuthClient CreateAuthClient() => new FlurlTbAuthClient(CreateRequestBuilder());
}
