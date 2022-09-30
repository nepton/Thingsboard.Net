using System.ComponentModel.Design;
using Microsoft.Extensions.DependencyInjection;
using Thingsboard.Sdk.DependencyInjection;

namespace Thingsboard.ClientSdk.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        IServiceCollection service = new ServiceCollection();
        service.AddThingsboardSdk(options =>
        {
            options.Url      = "https://tb.charger.aidncs.com";
            options.Username = "charger@aidncs.com";
            options.Password = "oulida123";
        });
        
        var provider = service.BuildServiceProvider();
        
    }
}
