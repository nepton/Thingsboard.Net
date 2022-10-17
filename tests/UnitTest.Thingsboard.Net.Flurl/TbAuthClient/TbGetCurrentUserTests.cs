namespace UnitTest.Thingsboard.Net.Flurl.TbAuthClient;

public class TbGetCurrentUserTests
{
    [Fact]
    public async Task TestGetCurrentUserWithDefaultOptions()
    {
        // arrange
        var options = TbTestFactory.Instance.Options;
        var authApi = TbTestFactory.Instance.CreateAuthClient();

        var userInfo = await authApi.GetCurrentUserAsync();
        Assert.NotNull(userInfo);
        Assert.Equal(options.Username, userInfo.Name);
    }
}
