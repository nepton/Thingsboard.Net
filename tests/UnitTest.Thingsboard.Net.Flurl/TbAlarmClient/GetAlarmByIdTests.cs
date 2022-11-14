namespace UnitTest.Thingsboard.Net.Flurl.TbAlarmClient;

/// <summary>
/// This class is used to test GetAlarmByIdAsync API
/// We should do the test in the following items:
/// 1. Get the correct alarm id
/// 2. Get the incorrect alarm id
/// </summary>
[Collection(nameof(TbTestCollection))]
public class GetAlarmByIdTests
{
    private readonly TbTestFixture _fixture;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public GetAlarmByIdTests(TbTestFixture fixture)
    {
        _fixture = fixture;
    }

    /// <summary>
    /// This test is used to test GetAlarmByIdAsync API with correct alarm id
    /// </summary>
    [Fact]
    public async Task TestGetAlarmByIdAsync()
    {
        //Arrange
        var client = TbTestFactory.Instance.CreateAlarmClient();
        var alarm  = await AlarmUtility.CreateNewAlarmAsync(_fixture.DeviceId);

        // Act
        var result = await client.GetAlarmByIdAsync(alarm.Id.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(alarm.Id,         result.Id);
        Assert.Equal(alarm.Type,       result.Type);
        Assert.Equal(alarm.Originator, result.Originator);
        Assert.Equal(alarm.Severity,   result.Severity);
        Assert.Equal(alarm.Status,     result.Status);
        Assert.Equal(alarm.Details,    result.Details);

        // 2. Delete the alarm
        await client.DeleteAlarmAsync(alarm.Id.Id);
    }

    [Fact]
    public async Task TestGetIncorrectAlarmByIdAsync()
    {
        //Arrange
        var client = TbTestFactory.Instance.CreateAlarmClient();

        // Act
        var result = await client.GetAlarmByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }
}
