using System.Net;
using Microsoft.Extensions.Options;
using Thingsboard.Net;
using Thingsboard.Net.Exceptions;
using Thingsboard.Net.Flurl.Options;

namespace UnitTest.Thingsboard.Net.Flurl.TbLoginClient;

public class TbLoginTests
{
    /// <summary>
    /// Test login with username and password to localhost
    /// </summary>
    [Fact]
    public async Task TestLoginApi()
    {
        // arrange
        var options  = TbTestFactory.Instance.Options;
        var loginApi = TbTestFactory.Instance.CreateLoginClient();

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
        var loginApi = TbTestFactory.Instance.CreateLoginClient();

        // act
        var ex = await Assert.ThrowsAsync<TbHttpException>(async () => await loginApi.LoginAsync(new TbLoginUser("wrongUsername", "wrongPassword")));

        // assert
        Assert.Equal(HttpStatusCode.Unauthorized, ex.StatusCode);
    }
}
