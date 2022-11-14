using Thingsboard.Net;
using Thingsboard.Net.Exceptions;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbTelemetryClient;

/// <summary>
/// This class is used to test the deleteDeviceAttributes method of the TbDeviceAttributesClient class.
/// 
/// We test following scenarios:
/// 1: Delete an alarm that exists.
/// 2: Delete an alarm that does not exist.
/// </summary>
[Collection(nameof(TbTestCollection))]
public class DeleteDeviceAttributesTests
{
    private readonly TbTestFixture _fixture;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public DeleteDeviceAttributesTests(TbTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task TestDeleteExistsDeviceAttributes()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();

        // Act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.SaveDeviceAttributesAsync(_fixture.DeviceId, TbAttributeScope.SERVER_SCOPE, new {testId = 100});
            await client.DeleteDeviceAttributesAsync(_fixture.DeviceId, TbAttributeScope.SERVER_SCOPE, new[] {"testId"});
        });

        // Assert
        Assert.Null(ex);
    }

    [Fact]
    public async Task TestDeleteClientSideDeviceAttributes()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();

        // Act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.DeleteDeviceAttributesAsync(_fixture.DeviceId, TbAttributeScope.CLIENT_SCOPE, new[] {"testId"});
        });

        // Assert
        Assert.Null(ex);
    }

    [Fact]
    public async Task TestDeleteNotExistsDeviceAttributes()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();

        // Act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.DeleteDeviceAttributesAsync(_fixture.CustomerId, TbAttributeScope.SERVER_SCOPE, new[] {"testId123"});
        });

        // Assert
        Assert.IsType<TbEntityNotFoundException>(ex);
    }

    [Fact]
    public async Task TestDeleteEmptyDeviceAttributes()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();

        // Act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.DeleteDeviceAttributesAsync(_fixture.DeviceId, TbAttributeScope.SERVER_SCOPE, Array.Empty<string>());
        });

        // Assert
        Assert.IsType<ArgumentException>(ex);
    }

    [Fact]
    public async Task TestDeleteNullDeviceAttributes()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();

        // Act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.DeleteDeviceAttributesAsync(_fixture.DeviceId, TbAttributeScope.SERVER_SCOPE, null!);
        });

        // Assert
        Assert.IsType<ArgumentNullException>(ex);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(
            TbTestFactory.Instance.CreateTelemetryClient(),
            async client =>
            {
                await client.DeleteDeviceAttributesAsync(Guid.NewGuid(), TbAttributeScope.SERVER_SCOPE, new[] {"testId123"});
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(
            TbTestFactory.Instance.CreateTelemetryClient(),
            async client =>
            {
                await client.DeleteDeviceAttributesAsync(Guid.NewGuid(), TbAttributeScope.SERVER_SCOPE, new[] {"testId123"});
            });
    }
}
