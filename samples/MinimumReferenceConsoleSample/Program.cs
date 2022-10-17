// This is the minimum dependency version

using Thingsboard.Net.Flurl;
using Thingsboard.Net.Flurl.Options;

// This is the minimum dependency version.
// You just need reference one package:
// <PackageReference Include="Thingsboard.Net.Flurl" Version="3.4.1.1" />

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
