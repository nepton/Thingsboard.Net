using Thingsboard.Net;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbEntityQueryClient;

/// <summary>
/// This class is used to test change password functionality
/// </summary>
[Collection(nameof(TbTestCollection))]
public class CountEntityDataByQueryTests
{
    private readonly TbTestFixture _fixture;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public CountEntityDataByQueryTests(TbTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task TestCountEntityDataByQuery()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateEntityQueryClient();

        // act
        var actual = await client.CountEntityDataByQueryAsync(new TbCountEntityDataRequest
        {
            EntityFilter = new TbSingleEntityFilter(TbEntityType.DEVICE, _fixture.DeviceId)
        });

        // assert
        Assert.Equal(1, actual);
    }

    [Fact]
    public async Task TestCountEntityDataByQueryWhenEmpty()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateEntityQueryClient();

        // act
        var actual = await client.CountEntityDataByQueryAsync(new TbCountEntityDataRequest
        {
            EntityFilter = new TbSingleEntityFilter(TbEntityType.DEVICE, Guid.Empty),
        });

        // assert
        Assert.Equal(0, actual);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateEntityQueryClient(),
            async client =>
            {
                await client.CountEntityDataByQueryAsync(new TbCountEntityDataRequest
                {
                    EntityFilter = new TbSingleEntityFilter(TbEntityType.DEVICE, Guid.Empty),
                });
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateEntityQueryClient(),
            async client =>
            {
                await client.CountEntityDataByQueryAsync(new TbCountEntityDataRequest
                {
                    EntityFilter = new TbSingleEntityFilter(TbEntityType.DEVICE, Guid.Empty),
                });
            });
    }
}
