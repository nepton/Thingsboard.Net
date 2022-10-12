using System;
using Microsoft.Extensions.DependencyInjection;
using Thingsboard.Net.Flurl.Options;
using Thingsboard.Net.Flurl.Utilities;
using Thingsboard.Net.Flurl.Utilities.Implements;

namespace Thingsboard.Net.Flurl.DependencyInjection;

public static class ThingsboardNetServicesExtensions
{
    /// <summary>Adds HTTP Logging services.</summary>
    /// <param name="services">The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> for adding services.</param>
    /// <param name="configureOptions">A delegate to configure the <see cref="T:Microsoft.AspNetCore.HttpLogging.HttpLoggingOptions" />.</param>
    /// <returns></returns>
    public static IServiceCollection AddThingsboardNet(
        this IServiceCollection            services,
        Action<ThingsboardNetFlurlOptions> configureOptions)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));
        if (configureOptions == null)
            throw new ArgumentNullException(nameof(configureOptions));

        services.Configure(configureOptions);
        services.AddTransient<IRequestBuilder, FlurlRequestBuilder>();
        services.AddTransient<IAccessTokenRepository, InMemoryAccessTokenRepository>();

        services.AddTransient<ITbAlarmClient, FlurlTbAlarmClient>();
        services.AddTransient<ITbAuthClient, FlurlTbAuthApi>();
        services.AddTransient<ITbDeviceClient, FlurlTbDeviceClient>();
        services.AddTransient<ITbEntityQueryClient, FlurlTbEntityQueryClient>();
        services.AddTransient<ITbLoginClient, FlurlTbLoginClient>();
        services.AddTransient<ITbTelemetryClient, FlurlTbTelemetryClient>();
        services.AddTransient<ITbRpcClient, FlurlTbRpcClient>();
        services.AddTransient<ITbAssetClient, FlurlTbAssetClient>();
        services.AddTransient<ITbCustomerClient, FlurlTbCustomerClient>();
        services.AddTransient<ITbDashboardClient, FlurlTbDashboardClient>();
        services.AddTransient<ITbAuditLogClient, FlurlTbAuditLogClient>();
        services.AddTransient<ITbQueueClient, FlurlTbQueueClient>();

        return services;
    }
}
