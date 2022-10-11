namespace Thingsboard.Net.Tests.TbDashboardClient;

public class GetServerTimeTester
{
    [Fact]
    public async Task TestGetServerTimeAsync()
    {
        // arrange
        var service = new TbTestService();
        var api     = service.GetRequiredService<ITbDashboardClient>();

        // act
        var now = await api.GetServerTimeAsync();

        // assert
        Assert.True((now - DateTime.UtcNow).TotalSeconds < 1);
    }
}
