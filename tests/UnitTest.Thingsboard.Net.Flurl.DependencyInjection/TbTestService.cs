using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Thingsboard.Net;
using Thingsboard.Net.Flurl.DependencyInjection;
using Thingsboard.Net.Flurl.Options;

namespace UnitTest.Thingsboard.Net.Flurl.DependencyInjection;

/// <summary>
/// The class for testing the Thingsboard client.
/// </summary>
public class TbTestService : IDisposable
{
    private readonly ServiceProvider _service;

    public ThingsboardNetFlurlOptions DefaultOptions { get; } = new()
    {
        BaseUrl  = "http://localhost:8080",
        Username = "tenant@thingsboard.org",
        Password = "tenant",
    };

    public TbTestService()
    {
        _service = BuildServiceProvider(options =>
        {
            options.BaseUrl  = DefaultOptions.BaseUrl;
            options.Username = DefaultOptions.Username;
            options.Password = DefaultOptions.Password;
        });
    }

    public TbTestService(Action<ThingsboardNetFlurlOptions> options)
    {
        _service = BuildServiceProvider(options);
    }

    private ServiceProvider BuildServiceProvider(Action<ThingsboardNetFlurlOptions> options)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();

        var services = new ServiceCollection();
        services.AddLogging(builder => builder.AddSerilog());
        services.AddThingsboardNet(options);

        return services.BuildServiceProvider();
    }

    public T GetRequiredService<T>() where T : notnull
    {
        return _service.GetRequiredService<T>();
    }

    public Task<TbDevice?> GetDeviceByNameAsync(string deviceName, CancellationToken cancel = default)
    {
        var deviceClient = GetRequiredService<ITbDeviceClient>();
        return deviceClient.GetTenantDeviceByNameAsync(deviceName, cancel);
    }

    /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
    public void Dispose()
    {
        _service.Dispose();
    }
}
