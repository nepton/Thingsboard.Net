[![Build status](https://ci.appveyor.com/api/projects/status/6yuxsfe71po3ofqg?svg=true)](https://ci.appveyor.com/project/nepton/thingsboard-net)
![GitHub issues](https://img.shields.io/github/issues/nepton/thingsboard-net.svg)
[![Tb.Net.Abstr nuget](https://img.shields.io/nuget/v/Thingsboard.Net.Flurl.svg?label=Tb.Net.Abstr%20nuget)](https://www.nuget.org/packages/Thingsboard.Net.Abstractions/)
[![Tb.Net.Flurl nuget](https://img.shields.io/nuget/v/Thingsboard.Net.Flurl.svg?label=Tb.Net.Flurl%20nuget)](https://www.nuget.org/packages/Thingsboard.Net.Flurl/)
[![Tb.Net.Flurl.DI nuget](https://img.shields.io/nuget/v/Thingsboard.Net.Flurl.DependencyInjection.svg?label=Tb.Net.Flurl.DI%20nuget)](https://www.nuget.org/packages/Thingsboard.Net.Flurl.DependencyInjection/)

Thingsboard.NET is a .NET client library for Thingsboard IoT Platform. It is a .NET Standard 2.0 library, so it can be used in .NET Core and .NET Framework applications.

## Installing
Install the NuGet package [Thingsboard.Net.Flurl](https://www.nuget.org/packages/Thingsboard.NET.Flurl/).

```
PM> Install-Package Thingsboard.NET.Flurl
```

## Usage
Creating a client and trying to invoke getCurrentUser method:

### Basic usage
```csharp
// Initial factory
var factory = new FlurlTbClientFactory
{
    Options = new ThingsboardNetFlurlOptions()
    {
        BaseUrl  = "http://localhost:8080",
        Username = "tenant@thingsboard.org",
        Password = "tenant",
    }
};

// Get the client
var authClient = factory.CreateAuthClient();
var userInfo = await authClient.GetCurrentUserAsync();
Console.WriteLine($"Hello {userInfo.Email}");
```

You will get the output from console:
```
Hello tenant@thingsboard.org
```

### Integration to ASP.NET Core
You can use the Thingsboard.NET.Flurl library in ASP.NET Core applications. Te dependency injection mode is supported.

First, add the Thingsboard.NET.Flurl.DependencyInjection library to your project:

```
PM> Install-Package Thingsboard.NET.Flurl.DependencyInjection
```

Then, register the Thingsboard.NET.Flurl services in the ConfigureServices method of Startup.cs:

```csharp
// add package "Thingsboard.Net.Flurl.DependencyInjection" Version="3.4.1.1"
serviceBuilder.AddThingsboardNet(options =>
{
    options.Username = "tenant@thingsboard.org";
    options.Password = "tenant";
    options.BaseUrl  = "http://localhost:8080";
});
```

Then you can inject the client factory in your controllers:

```csharp
public class HomeController : Controller
{
    private readonly ITbAuthClient _authClient;

    public HomeController(ITbAuthClient authClient)
    {
        _authClient = authClient;
    }

    public async Task<IActionResult> Index()
    {
        var userInfo = await _authClient.GetCurrentUserAsync();
        return View(userInfo);
    }
}
```

### Customization options
You can customize the client options before invoke RPC methods:

```csharp
public class HomeController : Controller
{
    private readonly ITbAuthClient _authClient;

    public HomeController(ITbAuthClient authClient)
    {
        _authClient = authClient;
    }

    public async Task<IActionResult> Index()
    {
        var userInfo = await auth
            .WithCredentials("newuser@thingsboard.com", "your-password")
            .WithBaseUrl("https://tb-server")
            .GetCurrentUserAsync();
        return View(userInfo);
    }
}
```

Leave a comment on GitHub if you have any questions or suggestions.

## Thanks
* Thanks to [Flurl](https://flurl.dev/) for the great HTTP library.
* Thanks to [Thingsboard](https://thingsboard.io/) for the great IoT platform.
* Thanks to [Polly](https://github.com/App-vNext/Polly) for the great resilience and transient-fault-handling library.

## License
This project is licensed under the MIT License
