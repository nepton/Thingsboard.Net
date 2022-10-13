using System.Net;
using Thingsboard.Net;
using Thingsboard.Net.Exceptions;

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
    /// Save new alarm with a valid alarm object.
    /// </summary>
    [Fact]
    public async Task TestSaveNewAlarm()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAlarmClient();

        // act
        var alarm    = AlarmUtility.GenerateEntity();
        var newAlarm = await client.SaveAlarmAsync(alarm);

        // assert
        Assert.NotNull(newAlarm);
        Assert.NotNull(newAlarm.Id);
        // Assert.Equal(alarm.Name,       newAlarm.Name); This is the _bug from thingsboard v3.4.1. The type is assigned to the name  
        Assert.Equal(alarm.Type,       newAlarm.Type);
        Assert.Equal(alarm.Originator, newAlarm.Originator);
        Assert.Equal(alarm.Severity,   newAlarm.Severity);
        Assert.Equal(alarm.Status,     newAlarm.Status);
        Assert.Equal(alarm.Details,    newAlarm.Details);

        // cleanup
        await client.DeleteAlarmAsync(newAlarm.Id!.Id);
    }

    /// <summary>
    /// Save new alarm with an invalid alarm object.
    /// </summary>
    [Fact]
    public async Task TestSaveInvalidAlarm()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAlarmClient();

        // act
        var alarm = AlarmUtility.GenerateEntity();
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
        var alarm = AlarmUtility.GenerateEntity();
        alarm.Originator = null;
        var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await client.SaveAlarmAsync(null!));

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
        var newAlarm = await AlarmUtility.CreateAlarmAsync();

        // act
        newAlarm.Propagate = !newAlarm.Propagate;
        var updatedAlarm = await client.SaveAlarmAsync(newAlarm);

        // assert
        Assert.NotNull(updatedAlarm);
        Assert.Equal(newAlarm.Propagate, updatedAlarm.Propagate);
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
        var alarm  = AlarmUtility.GenerateEntity();
        alarm.Id = new TbEntityId(TbEntityType.ALARM, Guid.NewGuid());

        // act
        var ex = await Assert.ThrowsAsync<TbEntityNotFoundException>(async () => await client.SaveAlarmAsync(alarm));

        // assert
        Assert.NotNull(ex);
        Assert.Equal(alarm.Id, ex.EntityId);
    }

    [Fact]
    public async Task TestInvalidUsername()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAlarmClient();

        // act
        var ex = await Assert.ThrowsAsync<TbHttpException>(async () =>
        {
            await client
                .WithCredentials(Guid.NewGuid().ToString(), Guid.NewGuid().ToString())
                .SaveAlarmAsync(AlarmUtility.GenerateEntity());
        });

        Assert.Equal(HttpStatusCode.Unauthorized, ex.StatusCode);
    }

    [Fact]
    public async Task TestInvalidBaseUrl()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAlarmClient();

        var ex = await Assert.ThrowsAsync<TbHttpException>(async () =>
        {
            await client
                .WithBaseUrl("http://localhost:123")
                .WithHttpTimeout(1, 0, 0)
                .SaveAlarmAsync(AlarmUtility.GenerateEntity());
        });

        Assert.Equal(false, ex.Completed);
    }
}
