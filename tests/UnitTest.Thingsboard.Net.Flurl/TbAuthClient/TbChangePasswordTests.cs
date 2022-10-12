using System.Net;
using Thingsboard.Net;
using Thingsboard.Net.Exceptions;

namespace UnitTest.Thingsboard.Net.Flurl.TbAuthClient;

/// <summary>
/// This class is used to test change password functionality
/// </summary>
public class TbChangePasswordTests
{
    [Fact]
    public async Task TestChangePasswordWithException()
    {
        var authApi = TbTestFactory.Instance.CreateAuthClient();

        var ex = await Assert.ThrowsAsync<TbHttpException>(async () => await authApi.ChangePasswordAsync(new TbChangePasswordRequest("tenant", "tenant")));
        Assert.Equal(HttpStatusCode.BadRequest,                         ex.StatusCode);
        Assert.Equal("New password should be different from existing!", ex.Message);
    }

    [Fact]
    public async Task TestChangePasswordWithWrongCurrentPassword()
    {
        var authApi = TbTestFactory.Instance.CreateAuthClient();

        var ex = await Assert.ThrowsAsync<TbHttpException>(async () => await authApi.ChangePasswordAsync(new TbChangePasswordRequest("WrongCurrentPassword", "tenant")));
        Assert.Equal(HttpStatusCode.BadRequest,         ex.StatusCode);
        Assert.Equal("Current password doesn't match!", ex.Message);
    }
}
