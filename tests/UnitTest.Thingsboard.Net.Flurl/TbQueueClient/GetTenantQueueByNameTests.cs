using Thingsboard.Net;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbQueueClient;

public class GetTenantQueueByNameTests
{
    [Fact]
    public async Task TestGetTenantQueueByName()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateQueueClient();
        var queues = await client.GetTenantQueuesByServiceTypeAsync(TbQueueServiceType.TB_RULE_ENGINE, 20, 0);
        Assert.NotNull(queues);
        Assert.NotEmpty(queues.Data);

        // act
        var actual = await client.GetQueueByNameAsync(queues.Data[0].Name!);

        Assert.NotNull(actual);
        Assert.Equal(queues.Data[0].Name, actual.Name);
    }

    [Fact]
    public async Task TestWhenQueueNotFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateQueueClient();

        // act
        var actual = await client.GetQueueByNameAsync(Guid.NewGuid().ToString());

        Assert.Null(actual);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateQueueClient(),
            async client =>
            {
                await client.GetQueueByNameAsync(string.Empty);
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateQueueClient(),
            async client =>
            {
                await client.GetQueueByNameAsync(string.Empty);
            });
    }
}
