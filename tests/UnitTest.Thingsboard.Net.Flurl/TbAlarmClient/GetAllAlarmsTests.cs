using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbAlarmClient;

/// <summary>
/// This class is used to test the GetAlarms method of <see cref="TbAlarmClient"/> class.
/// We will following scenarios:
/// 1. Get all entities with limit.
/// 2. Get nothing has right response.
/// </summary>
public class GetAllAlarmsTests
{
    [Fact]
    public async Task TestGetAlarms()
    {
        // arrange
        var client   = TbTestFactory.Instance.CreateAlarmClient();
        var newAlarm = await AlarmUtility.CreateNewAlarmAsync();

        // act
        var entities = await client.GetAllAlarmsAsync(20, 0, textSearch: newAlarm.Name);

        // assert
        Assert.NotNull(entities);
        Assert.NotEmpty(entities.Data);
        Assert.Equal(1, entities.TotalElements);
        Assert.Single(entities.Data);
        Assert.Equal(newAlarm.Name, entities.Data[0].Name);

        // clean up
        await client.DeleteAlarmAsync(newAlarm.Id!.Id);
    }

    [Fact]
    public async Task TestGetNothing()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAlarmClient();

        // act
        var entities = await client.GetAllAlarmsAsync(20, 0, textSearch: Guid.NewGuid().ToString());

        // assert
        Assert.NotNull(entities);
        Assert.Empty(entities.Data);
        Assert.Equal(0, entities.TotalElements);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(
            TbTestFactory.Instance.CreateAlarmClient(),
            async client =>
            {
                await client.GetAllAlarmsAsync(20, 0, textSearch: Guid.NewGuid().ToString());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(
            TbTestFactory.Instance.CreateAlarmClient(),
            async client =>
            {
                await client.GetAllAlarmsAsync(20, 0, textSearch: Guid.NewGuid().ToString());
            });
    }
}
