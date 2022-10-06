using Microsoft.Extensions.Options;
using Thingsboard.Net.DependencyInjection;
using Thingsboard.Net.TbLogin;

namespace Thingsboard.Net.Tests.TbLogin;

public class TbLoginTest
{
    [Fact]
    public async Task TestLoginApi()
    {
        // arrange
        using var service  = new TbService();
        var       options  = service.GetRequiredService<IOptions<ThingsboardNetOptions>>().Value;
        var       loginApi = service.GetRequiredService<ITbLoginApi>();

        // act
        var loginRequest  = new TbLoginRequest(options.Username!, options.Password!);
        var loginResponse = await loginApi.LoginAsync(loginRequest);

        // assert
        Assert.NotNull(loginResponse);
        Assert.NotNull(loginResponse.Token);
        Assert.NotNull(loginResponse.RefreshToken);
    }
}
