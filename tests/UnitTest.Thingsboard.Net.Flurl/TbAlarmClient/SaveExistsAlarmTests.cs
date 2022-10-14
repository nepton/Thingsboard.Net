using System.Net;
using Thingsboard.Net;
using Thingsboard.Net.Exceptions;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbAlarmClient;

/// <summary>
/// This class is used to test the saveAlarm methods of the TbAlarmClient class.
/// We will following scenarios:
/// 1. Save new alarm with a valid alarm object.
/// 2. Save new alarm with an invalid alarm object.
/// 3. Save new alarm with a null alarm object.
/// 4. Update an exists alarm with a valid alarm object.
/// 5. Update an not exists alarm with a valid alarm object.
/// </summary>
public class SaveAlarmTests
{
    /// <summary>
    /// Save new alarm with an invalid alarm object.
    /// </summary>
    [Fact]
    public async Task TestSaveInvalidAlarm()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAlarmClient();
        var alarm  = await AlarmUtility.CreateNewAlarmAsync();

        // act
        alarm.Originator = null;
        var ex = await Assert.ThrowsAsync<TbHttpException>(async () => await client.SaveAlarmAsync(alarm));

        // assert
        Assert.NotNull(ex);
        Assert.Equal(HttpStatusCode.BadRequest,               ex.StatusCode);
        Assert.Equal("Alarm originator should be specified!", ex.Message);
    }

    /// <summary>
    /// Save new alarm with an invalid alarm object.
    /// </summary>
    [Fact]
    public async Task TestSaveNullAlarm()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAlarmClient();

        // act
        var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await client.SaveAlarmAsync(default(TbAlarm)!));

        // assert
        Assert.NotNull(ex);
    }

    /// <summary>
    /// Update an exists alarm with a valid alarm object.
    /// </summary>
    /// <returns></returns>
    [Fact]
    private async Task TestUpdateAlarmAsync()
    {
        // arrange
        var client   = TbTestFactory.Instance.CreateAlarmClient();
        var expected = await AlarmUtility.CreateNewAlarmAsync();

        // act
        expected.Propagate = !expected.Propagate;
        var actual = await client.SaveAlarmAsync(expected);

        // assert
        Assert.NotNull(actual);
        Assert.Equal(expected.Propagate, actual.Propagate);
    }

    /// <summary>
    /// Update an not exists alarm with a valid alarm object.
    /// </summary>
    /// <returns></returns>
    [Fact]
    private async Task TestUpdateNotExistsAlarmAsync()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAlarmClient();
        var alarm  = new TbAlarm(new TbEntityId(TbEntityType.ALARM, Guid.NewGuid()));

        // act
        var ex = await Assert.ThrowsAsync<TbEntityNotFoundException>(async () => await client.SaveAlarmAsync(alarm));

        // assert
        Assert.NotNull(ex);
        Assert.Equal(alarm.Id, ex.EntityId);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(
            TbTestFactory.Instance.CreateAlarmClient(),
            async client =>
            {
                await client.SaveAlarmAsync(AlarmUtility.GenerateNewAlarm());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(
            TbTestFactory.Instance.CreateAlarmClient(),
            async client =>
            {
                await client.SaveAlarmAsync(AlarmUtility.GenerateNewAlarm());
            });
    }
}
