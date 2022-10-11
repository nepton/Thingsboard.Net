using System.Net;
using Microsoft.Extensions.Options;
using Thingsboard.Net.Exceptions;
using Thingsboard.Net.Flurl.Options;

namespace Thingsboard.Net.Tests.TbLoginClient;

public class TbLoginTests
{
    /// <summary>
    /// Test login with username and password to localhost
    /// </summary>
    [Fact]
    public async Task TestLoginApi()
    {
        // arrange
        using var service  = new TbTestService();
        var       options  = service.GetRequiredService<IOptions<ThingsboardNetFlurlOptions>>().Value;
        var       loginApi = service.GetRequiredService<ITbLoginClient>();

        // act
        var loginRequest  = new TbLoginUser(options.Username!, options.Password!);
        var loginResponse = await loginApi.LoginAsync(loginRequest);

        // assert
        Assert.NotNull(loginResponse);
        Assert.NotNull(loginResponse.Token);
        Assert.NotNull(loginResponse.RefreshToken);
    }

    [Fact]
    public async Task TestIfUsernameIncorrect()
    {
        // arrange
        using var service  = new TbTestService();
        var       options  = service.GetRequiredService<IOptions<ThingsboardNetFlurlOptions>>().Value;
        var       loginApi = service.GetRequiredService<ITbLoginClient>();

        // act
        var ex = await Assert.ThrowsAsync<TbHttpException>(async () => await loginApi.LoginAsync(new TbLoginUser("wrongUsername", options.Password!)));

        // assert
        Assert.Equal(HttpStatusCode.Unauthorized, ex.StatusCode);
    }
}
