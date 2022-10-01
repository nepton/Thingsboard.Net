using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;
using Thingsboard.Net.DependencyInjection;
using Thingsboard.Net.TbAuth;

namespace Thingsboard.ClientSdk.Tests;

public class UnitTest1
{
    [Fact]
    public async Task Test1()
    {
        Log.Logger = new LoggerConfiguration().CreateLogger();

        IServiceCollection serviceBuilder = new ServiceCollection();
        serviceBuilder.AddLogging(configure => configure.AddSerilog());
        serviceBuilder.AddThingsboardSdk(options =>
        {
            options.Url      = "";
            options.Username = "";
            options.Password = "";
        });

        var service       = serviceBuilder.BuildServiceProvider();
        var options       = service.GetRequiredService<IOptions<ThingsboardNetOptions>>().Value;
        var auth          = service.GetRequiredService<ITbAuth>();
        var loginResponse = await auth.LoginAsync(new TbLoginRequest(options.Username!, options.Password!));
        Assert.NotNull(loginResponse);
    }
}
