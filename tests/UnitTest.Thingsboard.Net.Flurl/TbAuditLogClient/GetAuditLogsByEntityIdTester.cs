using Thingsboard.Net;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;
using UnitTest.Thingsboard.Net.Flurl.TbDeviceClient;

namespace UnitTest.Thingsboard.Net.Flurl.TbAuditLogClient;

[Collection(nameof(TbTestCollection))]
public class GetAuditLogsByEntityIdTester
{
    private readonly TbTestFixture _fixture;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public GetAuditLogsByEntityIdTester(TbTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task TestGetAuditLogsByEntityId()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAuditLogClient();

        // add device for log audit
        var deviceClient = TbTestFactory.Instance.CreateDeviceClient();
        var newDevice    = await deviceClient.SaveDeviceAsync(DeviceUtility.GenerateEntity(_fixture.DeviceProfileId));

        // act
        var actual = await client.GetAuditLogsByEntityIdAsync(TbEntityType.DEVICE, newDevice.Id.Id, 20, 0);

        // assert
        Assert.NotNull(actual);
        Assert.NotEmpty(actual.Data);

        // clean up
        await deviceClient.DeleteDeviceAsync(newDevice.Id.Id);
    }

    [Fact]
    public async Task TestWhenNoDataFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAuditLogClient();

        // act
        var actual = await client.GetAuditLogsByEntityIdAsync(TbEntityType.CUSTOMER, _fixture.CustomerId, 20, 0, textSearch: Guid.NewGuid().ToString());

        // assert
        Assert.NotNull(actual);
        Assert.Empty(actual.Data);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateAuditLogClient(),
            async client =>
            {
                await client.GetAuditLogsByEntityIdAsync(TbEntityType.CUSTOMER, _fixture.CustomerId, 20, 0, textSearch: Guid.NewGuid().ToString());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateAuditLogClient(),
            async client =>
            {
                await client.GetAuditLogsByEntityIdAsync(TbEntityType.CUSTOMER, _fixture.CustomerId, 20, 0, textSearch: Guid.NewGuid().ToString());
            });
    }
}
