using FluentAssertions;
using Thingsboard.Net;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbEntityQueryClient;

/// <summary>
/// This class is used to test change password functionality
/// </summary>
public class FindEntityDataByQueryTests
{
    [Fact]
    public async Task TestFindEntityDataByQuery()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateEntityQueryClient();

        // act
        var labelField = new TbEntityField("label", TbEntityFieldType.ENTITY_FIELD);
        var nameField  = new TbEntityField("name",  TbEntityFieldType.ENTITY_FIELD);
        var actual = await client.FindEntityDataByQueryAsync(new TbFindEntityDataRequest
        {
            EntityFilter = new TbSingleEntityFilter(TbEntityType.DEVICE, TbTestData.TestDeviceId),
            EntityFields = new[] {labelField, nameField},
            PageLink = new TbEntityDataPageLink
            {
                Page     = 0,
                PageSize = 20,
            }
        });

        // assert
        actual.Data.Should().NotBeNull().And.NotBeNull();
        actual.Data[0].Latest?.Get(nameField)?.Value?.To<string>().Should().Be(TbTestData.TestDeviceName);
    }

    [Fact]
    public async Task TestFindEntityDataByQueryWhenEmpty()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateEntityQueryClient();

        // act
        var actual = await client.FindEntityDataByQueryAsync(new TbFindEntityDataRequest
        {
            EntityFilter = new TbSingleEntityFilter(TbEntityType.DEVICE, Guid.Empty),
            EntityFields = new[] {new TbEntityField("id", TbEntityFieldType.ENTITY_FIELD)},
            PageLink = new TbEntityDataPageLink
            {
                Page     = 0,
                PageSize = 20,
            }
        });

        // assert
        Assert.NotNull(actual);
        Assert.Empty(actual.Data);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateEntityQueryClient(),
            async client =>
            {
                await client.FindEntityDataByQueryAsync(new TbFindEntityDataRequest
                {
                    EntityFilter = new TbSingleEntityFilter(TbEntityType.DEVICE, Guid.Empty),
                    EntityFields = new[] {new TbEntityField("id", TbEntityFieldType.ENTITY_FIELD)},
                    PageLink = new TbEntityDataPageLink
                    {
                        Page     = 0,
                        PageSize = 20,
                    }
                });
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateEntityQueryClient(),
            async client =>
            {
                await client.FindEntityDataByQueryAsync(new TbFindEntityDataRequest
                {
                    EntityFilter = new TbSingleEntityFilter(TbEntityType.DEVICE, Guid.Empty),
                    EntityFields = new[] {new TbEntityField("id", TbEntityFieldType.ENTITY_FIELD)},
                    PageLink = new TbEntityDataPageLink
                    {
                        Page     = 0,
                        PageSize = 20,
                    }
                });
            });
    }
}
