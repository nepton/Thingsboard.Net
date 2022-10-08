using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Thingsboard.Net.Flurl.DependencyInjection;
using Thingsboard.Net.Options;

namespace Thingsboard.Net.Tests;

/// <summary>
/// The class for testing the Thingsboard client.
/// </summary>
public class TbTestService : IDisposable
{
    private readonly ServiceProvider _service;

    public TbTestService()
    {
        _service = BuildServiceProvider(options =>
        {
            options.Url      = "http://localhost:8080";
            options.Username = "tenant@thingsboard.org";
            options.Password = "tenant";
        });
    }

    public TbTestService(Action<ThingsboardNetOptions> options)
    {
        _service = BuildServiceProvider(options);
    }

    private ServiceProvider BuildServiceProvider(Action<ThingsboardNetOptions> options)
    {
        var services = new ServiceCollection();
        services.AddLogging(builder => builder.AddSerilog());
        services.AddThingsboardNet(options);

        return services.BuildServiceProvider();
    }

    public T GetRequiredService<T>() where T : notnull
    {
        return _service.GetRequiredService<T>();
    }

    /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
    public void Dispose()
    {
        _service.Dispose();
    }
}
