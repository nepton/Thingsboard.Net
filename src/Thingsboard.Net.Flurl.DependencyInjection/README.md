Thingsboard.NET is a .NET client library for Thingsboard IoT Platform. It is a .NET Standard 2.0 library, so it can be used in .NET Core and .NET Framework applications.


## Installing
You can use the Thingsboard.NET.Flurl library in ASP.NET Core applications. Te dependency injection mode is supported.

First, add the Thingsboard.NET.Flurl.DependencyInjection library to your project:

```
PM> Install-Package Thingsboard.NET.Flurl.DependencyInjection
```

## Usage

### Integration to ASP.NET Core

Register the Thingsboard.NET.Flurl services in the ConfigureServices method of Startup.cs:

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
