﻿using System.Net;
using Microsoft.Extensions.Options;
using Thingsboard.Net.Exceptions;
using Thingsboard.Net.Flurl.Options;

namespace Thingsboard.Net.Tests.TbAuthClient;

public class TbGetCurrentUserTests
{
    [Fact]
    public async Task TestGetCurrentUserWithDefaultOptions()
    {
        // arrange
        using var service = new TbTestService();
        var       options = service.GetRequiredService<IOptions<ThingsboardNetFlurlOptions>>().Value;
        var       authApi = service.GetRequiredService<ITbAuthClient>();

        var userInfo = await authApi.GetCurrentUserAsync();
        Assert.NotNull(userInfo);
        Assert.Equal(options.Username, userInfo.Name);
    }

    [Fact]
    public async Task TestGetCurrentUserWithIncorrectUsernameInDefaultOptions()
    {
        // arrange
        using var service = new TbTestService(options =>
        {
            options.BaseUrl      = "http://localhost:8080";
            options.Username = "incorrect";
        });
        var authApi = service.GetRequiredService<ITbAuthClient>();

        var ex = await Assert.ThrowsAsync<TbHttpException>(async () => await authApi
            .GetCurrentUserAsync());
        Assert.Equal(HttpStatusCode.Unauthorized, ex.StatusCode);
    }

    [Fact]
    public async Task TestGetCurrentUserWithCustomOptions()
    {
        // arrange
        using var service = new TbTestService();
        var       authApi = service.GetRequiredService<ITbAuthClient>();

        var username = "sysadmin@thingsboard.org";
        var userInfo = await authApi
            .WithCredentials(username, "sysadmin")
            .GetCurrentUserAsync();
        Assert.NotNull(userInfo);
        Assert.Equal(username, userInfo.Name);
    }

    [Fact]
    public async Task TestGetCurrentUserWithInvalidCustomUsername()
    {
        // arrange
        using var service = new TbTestService();
        var       authApi = service.GetRequiredService<ITbAuthClient>();

        var username = "user_does_not_exists";
        var ex = await Assert.ThrowsAsync<TbHttpException>(async () => await authApi
            .WithCredentials(username, "")
            .GetCurrentUserAsync());
        Assert.Equal(HttpStatusCode.Unauthorized, ex.StatusCode);
    }
}