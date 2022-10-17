using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Thingsboard.Net;
using Thingsboard.Net.Flurl.DependencyInjection;

// -- Configuration -----------------------------------------------------------------------------------

// add package "Serilog" Version="2.12.0"
// add package "Microsoft.Extensions.Logging" Version="6.0.0" will fix NullLogger.BeginScope issue
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console() // add package "Serilog.Sinks.Console" Version = "4.1.0"
    .CreateLogger();

// add package "Microsoft.Extensions.DependencyInjection" Version="6.0.0"
var serviceBuilder = new ServiceCollection();

// add package "Serilog.Extensions.Logging" Version="3.1.0"
serviceBuilder.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());

// add package "Thingsboard.Net.Flurl.DependencyInjection" Version="3.4.1.1"
serviceBuilder.AddThingsboardNet(options =>
{
    options.Username = "tenant@thingsboard.org";
    options.Password = "tenant";
    options.BaseUrl  = "http://localhost:8080";
});
var services = serviceBuilder.BuildServiceProvider();

// -- Running -----------------------------------------------------------------------------------

// All api are declared as ITb + controllerName + Client
// Please see swagger at your thingsboard host http://your-tb-server/swagger-ui/
var auth = services.GetRequiredService<ITbAuthClient>();

var userInfo = await auth.GetCurrentUserAsync();
Console.WriteLine($"Hello {userInfo.Email}");
