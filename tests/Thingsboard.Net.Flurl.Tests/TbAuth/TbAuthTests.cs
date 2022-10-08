using Microsoft.Extensions.Options;
using Thingsboard.Net.Exceptions;
using Thingsboard.Net.Options;
using Thingsboard.Net.TbAuth;

namespace Thingsboard.Net.Tests.TbAuth;

public class TbAuthTests
{
    [Fact]
    public async Task TestGetLoginUserWithDefaultOptions()
    {
        // arrange
        using var service = new TbService();
        var       options = service.GetRequiredService<IOptions<ThingsboardNetOptions>>().Value;
        var       authApi = service.GetRequiredService<ITbAuthApi>();

        var userInfo = await authApi.GetCurrentUserAsync();
        Assert.NotNull(userInfo);
        Assert.Equal(options.Username, userInfo.Name);
    }

    [Fact]
    public async Task TestGetLoginUserWithIncorrectUsernameInDefaultOptions()
    {
        // arrange
        using var service = new TbService(options =>
        {
            options.Url      = "http://localhost:8080";
            options.Username = "incorrect";
        });
        var authApi = service.GetRequiredService<ITbAuthApi>();

        var ex = await Assert.ThrowsAsync<TbHttpException>(async () => await authApi
            .GetCurrentUserAsync());
        Assert.Equal(401, ex.StatusCode);
    }

    [Fact]
    public async Task TestGetLoginUserWithCustomOptions()
    {
        // arrange
        using var service = new TbService();
        var       authApi = service.GetRequiredService<ITbAuthApi>();

        var username = "sysadmin@thingsboard.org";
        var userInfo = await authApi
            .WithCredentials(username, "sysadmin")
            .GetCurrentUserAsync();
        Assert.NotNull(userInfo);
        Assert.Equal(username, userInfo.Name);
    }

    [Fact]
    public async Task TestGetLoginUserWithInvalidCustomUsername()
    {
        // arrange
        using var service = new TbService();
        var       authApi = service.GetRequiredService<ITbAuthApi>();

        var username = "user_does_not_exists";
        var ex = await Assert.ThrowsAsync<TbHttpException>(async () => await authApi
            .WithCredentials(username, "")
            .GetCurrentUserAsync());
        Assert.Equal(401, ex.StatusCode);
    }
}
