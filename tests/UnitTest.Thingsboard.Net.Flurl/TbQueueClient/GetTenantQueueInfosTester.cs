using Thingsboard.Net;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbQueueClient;

public class GetTenantQueueInfosTester
{
    [Fact]
    public async Task TestGetTenantQueueInfos()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateQueueClient();

        // act
        var queues = await client.GetTenantQueuesByServiceTypeAsync(TbQueueServiceType.TB_RULE_ENGINE, 20, 0);

        // assert
        Assert.NotNull(queues);
        Assert.NotEmpty(queues.Data);
    }

    [Fact]
    public async Task TestWhenNoDataFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateQueueClient();

        // act
        var queues = await client.GetTenantQueuesByServiceTypeAsync(TbQueueServiceType.TB_CORE, 20, 0, textSearch: Guid.NewGuid().ToString());

        // assert
        Assert.NotNull(queues);
        Assert.Empty(queues.Data);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateQueueClient(),
            async client =>
            {
                await client.GetTenantQueuesByServiceTypeAsync(TbQueueServiceType.TB_CORE, 20, 0, textSearch: Guid.NewGuid().ToString());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateQueueClient(),
            async client =>
            {
                await client.GetTenantQueuesByServiceTypeAsync(TbQueueServiceType.TB_CORE, 20, 0, textSearch: Guid.NewGuid().ToString());
            });
    }
}
