using FluentAssertions;
using Thingsboard.Net;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbEntityQueryClient;

/// <summary>
/// This class is used to test change password functionality
/// </summary>
[Collection(nameof(TbTestCollection))]
public class FindAlarmsDataByQueryTests
{
    private readonly TbTestFixture _fixture;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public FindAlarmsDataByQueryTests(TbTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task TestFindAlarmsDataByQuery()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateEntityQueryClient();

        // act
        var labelField = new TbEntityField("label", TbEntityFieldType.ENTITY_FIELD);
        var nameField  = new TbEntityField("name",  TbEntityFieldType.ENTITY_FIELD);
        var actual = await client.FindAlarmsByQueryAsync(new TbAlarmDataQueryRequest
        {
            EntityFilter = new TbSingleEntityFilter(TbEntityType.DEVICE, _fixture.DeviceId),
            EntityFields = new[] {labelField, nameField},
            PageLink = new TbAlarmDataPageLink
            {
                Page     = 0,
                PageSize = 20,
            }
        });

        // assert
        actual.Data.Should().NotBeNull().And.NotBeNull();
        // actual.Data[0].Latest?.Get(nameField)?.Value?.To<string>().Should().Be(_fixture.Device.Name);
    }

    [Fact]
    public async Task TestFindAlarmsDataByQueryWhenEmpty()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateEntityQueryClient();

        // act
        var actual = await client.FindAlarmsByQueryAsync(new TbAlarmDataQueryRequest
        {
            EntityFilter = new TbSingleEntityFilter(TbEntityType.DEVICE, Guid.Empty),
            EntityFields = new[] {new TbEntityField("id", TbEntityFieldType.ENTITY_FIELD)},
            PageLink = new TbAlarmDataPageLink
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
                await client.FindAlarmsByQueryAsync(new TbAlarmDataQueryRequest
                {
                    EntityFilter = new TbSingleEntityFilter(TbEntityType.DEVICE, Guid.Empty),
                    EntityFields = new[] {new TbEntityField("id", TbEntityFieldType.ENTITY_FIELD)},
                    PageLink = new TbAlarmDataPageLink
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
                await client.FindAlarmsByQueryAsync(new TbAlarmDataQueryRequest
                {
                    EntityFilter = new TbSingleEntityFilter(TbEntityType.DEVICE, Guid.Empty),
                    EntityFields = new[] {new TbEntityField("id", TbEntityFieldType.ENTITY_FIELD)},
                    PageLink = new TbAlarmDataPageLink
                    {
                        Page     = 0,
                        PageSize = 20,
                    }
                });
            });
    }
}
