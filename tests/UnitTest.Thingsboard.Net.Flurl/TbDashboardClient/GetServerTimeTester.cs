using Thingsboard.Net;

namespace UnitTest.Thingsboard.Net.Flurl.TbDashboardClient;

public class GetServerTimeTester
{
    [Fact]
    public async Task TestGetServerTimeAsync()
    {
        // arrange
        var api = TbTestFactory.Instance.CreateDashboardClient();

        // act
        var now = await api.GetServerTimeAsync();

        // assert
        Assert.True((now - DateTime.UtcNow).TotalSeconds < 1);
    }
}
