using Thingsboard.Net;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbQueueClient;

public class GetQueueByIdTests
{
    [Fact]
    public async Task TestGetQueueById()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateQueueClient();

        // act
        var queues = await client.GetTenantQueuesByServiceTypeAsync(TbQueueServiceType.TB_RULE_ENGINE, 20, 0);
        Assert.NotNull(queues);
        Assert.NotEmpty(queues.Data);
        var actual = await client.GetQueueByIdAsync(queues.Data[0].Id.Id);

        Assert.NotNull(actual);
        Assert.Equal(queues.Data[0].Id.Id, actual.Id.Id);
        Assert.Equal(queues.Data[0].Name,  actual.Name);
    }

    [Fact]
    public async Task TestWhenQueueNotFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateQueueClient();

        // act
        var actual  = await client.GetQueueByIdAsync(Guid.NewGuid());

        Assert.Null(actual);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateQueueClient(),
            async client =>
            {
                await client.GetQueueByIdAsync(Guid.NewGuid());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateQueueClient(),
            async client =>
            {
                await client.GetQueueByIdAsync(Guid.NewGuid());
            });
    }
}
