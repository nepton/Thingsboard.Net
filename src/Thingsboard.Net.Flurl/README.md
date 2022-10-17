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
