using System;
using Microsoft.Extensions.DependencyInjection;
using Thingsboard.Net.Flurl.TbAlarmController;
using Thingsboard.Net.Flurl.TbAuth;
using Thingsboard.Net.Flurl.TbDeviceController;
using Thingsboard.Net.Flurl.TbEntityQuery;
using Thingsboard.Net.Flurl.TbLogin;
using Thingsboard.Net.Flurl.Utility;
using Thingsboard.Net.Flurl.Utility.Implements;
using Thingsboard.Net.Options;
using Thingsboard.Net.TbAlarmController;
using Thingsboard.Net.TbAuthController;
using Thingsboard.Net.TbDeviceController;
using Thingsboard.Net.TbEntityQuery;
using Thingsboard.Net.TbLogin;

namespace Thingsboard.Net.Flurl.DependencyInjection;

public static class ThingsboardNetServicesExtensions
{
    /// <summary>Adds HTTP Logging services.</summary>
    /// <param name="services">The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> for adding services.</param>
    /// <param name="configureOptions">A delegate to configure the <see cref="T:Microsoft.AspNetCore.HttpLogging.HttpLoggingOptions" />.</param>
    /// <returns></returns>
    public static IServiceCollection AddThingsboardNet(
        this IServiceCollection       services,
        Action<ThingsboardNetOptions> configureOptions)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));
        if (configureOptions == null)
            throw new ArgumentNullException(nameof(configureOptions));

        services.Configure(configureOptions);
        services.AddTransient<IRequestBuilder, FlurlRequestBuilder>();
        services.AddTransient<IAccessToken, InMemoryCachedAccessToken>();

        services.AddTransient<ITbEntityQuery, FlurlTbEntityQuery>();
        services.AddTransient<ITbLoginApi, FlurlTbLoginApi>();
        services.AddTransient<ITbAuthApi, FlurlTbAuthApi>();
        services.AddTransient<ITbDeviceClient, FlurlTbDeviceClient>();
        services.AddTransient<ITbAlarmApi, FlurlTbAlarmApi>();

        return services;
    }
}
