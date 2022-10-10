using System.Net;
using Thingsboard.Net.Exceptions;

namespace Thingsboard.Net.Tests.TbAuthClient;

/// <summary>
/// This class is used to test change password functionality
/// </summary>
public class TbChangePasswordTests
{
    [Fact]
    public async Task TestChangePasswordWithException()
    {
        using var service = new TbTestService();
        var       authApi = service.GetRequiredService<ITbAuthClient>();

        var ex = await Assert.ThrowsAsync<TbHttpException>(async () => await authApi.ChangePasswordAsync(new TbChangePasswordRequest("tenant", "tenant")));
        Assert.Equal(HttpStatusCode.BadRequest,                         ex.StatusCode);
        Assert.Equal("New password should be different from existing!", ex.Message);
    }

    [Fact]
    public async Task TestChangePasswordWithWrongCurrentPassword()
    {
        using var service = new TbTestService();
        var       authApi = service.GetRequiredService<ITbAuthClient>();

        var ex = await Assert.ThrowsAsync<TbHttpException>(async () => await authApi.ChangePasswordAsync(new TbChangePasswordRequest("WrongCurrentPassword", "tenant")));
        Assert.Equal(HttpStatusCode.BadRequest,         ex.StatusCode);
        Assert.Equal("Current password doesn't match!", ex.Message);
    }
}
