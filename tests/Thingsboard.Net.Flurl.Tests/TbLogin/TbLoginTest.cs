using Microsoft.Extensions.Options;
using Thingsboard.Net.Exceptions;
using Thingsboard.Net.Options;
using Thingsboard.Net.TbLogin;

namespace Thingsboard.Net.Tests.TbLogin;

public class TbLoginTests
{
    /// <summary>
    /// Test login with username and password to localhost
    /// </summary>
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

    [Fact]
    public async Task TestIfUsernameIncorrect()
    {
        // arrange
        using var service  = new TbService();
        var       options  = service.GetRequiredService<IOptions<ThingsboardNetOptions>>().Value;
        var       loginApi = service.GetRequiredService<ITbLoginApi>();

        // act
        var ex = await Assert.ThrowsAsync<TbHttpException>(async () => await loginApi.LoginAsync(new TbLoginRequest("wrongUsername", options.Password!)));

        // assert
        Assert.Equal(401, ex.StatusCode);
    }
}
