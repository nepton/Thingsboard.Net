using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Thingsboard.Net.DependencyInjection;

namespace Thingsboard.Net.Tests;

/// <summary>
/// The class for testing the Thingsboard client.
/// </summary>
public class TbService : IDisposable
{
    private readonly ServiceProvider _service;

    public TbService()
    {
        Log.Logger = new LoggerConfiguration().CreateLogger();

        IServiceCollection serviceBuilder = new ServiceCollection();
        serviceBuilder.AddLogging(configure => configure.AddSerilog());
        serviceBuilder.AddThingsboardSdk(options =>
        {
            options.Url      = "http://localhost:8080";
            options.Username = "tenant@thingsboard.org";
            options.Password = "tenant";
        });

        _service = serviceBuilder.BuildServiceProvider();
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
