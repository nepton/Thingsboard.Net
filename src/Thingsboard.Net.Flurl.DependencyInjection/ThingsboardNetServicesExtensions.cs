using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Thingsboard.Net.Flurl.Options;
using Thingsboard.Net.Flurl.Utilities;

namespace Thingsboard.Net.Flurl.DependencyInjection;

public static class ThingsboardNetServicesExtensions
{
    /// <summary>
    /// Adds the thingsboard client sdk api services. 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration">The configuration contains the options of <see cref="T:ThingsboardNetFlurlOptions"/>.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddThingsboardNet(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));
        if (configuration == null) throw new ArgumentNullException(nameof(configuration));

        services.Configure<ThingsboardNetFlurlOptions>(configuration);
        AddCore(services);

        return services;
    }

    /// <summary>Adds Thingsboard client sdk api services.</summary>
    /// <param name="services">The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> for adding services.</param>
    /// <param name="configureOptions">A delegate to configure the <see cref="T:ThingsboardNetFlurlOptions" />.</param>
    /// <returns></returns>
    public static IServiceCollection AddThingsboardNet(this IServiceCollection services, Action<ThingsboardNetFlurlOptions> configureOptions)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));
        if (configureOptions == null)
            throw new ArgumentNullException(nameof(configureOptions));

        services.Configure(configureOptions);

        AddCore(services);

        return services;
    }

    private static void AddCore(IServiceCollection services)
    {
        services.AddTransient<IRequestBuilder, FlurlRequestBuilder>();
        services.AddTransient<IAccessTokenRepository, InMemoryAccessTokenRepository>();
        services.AddTransient<IOptionsReaderFactory, OptionsSnapshotReader>();

        services.AddTransient<ITbAlarmClient, FlurlTbAlarmClient>();
        services.AddTransient<ITbAuthClient, FlurlTbAuthClient>();
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
        services.AddTransient<ITbDeviceProfileClient, FlurlTbDeviceProfileClient>();
        services.AddTransient<ITbEntityRelationClient, FlurlTbEntityRelationClient>();
    }
}
