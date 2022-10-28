using Thingsboard.Net;
using Thingsboard.Net.Exceptions;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbTelemetryClient;

/// <summary>
/// This class is used to test the deleteEntityAttributes method of the TbEntityAttributesClient class.
/// 
/// We test following scenarios:
/// 1: Delete an alarm that exists.
/// 2: Delete an alarm that does not exist.
/// </summary>
public class DeleteEntityAttributesTests
{
    [Fact]
    public async Task TestDeleteExistsEntityAttributes()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();

        // Act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.SaveEntityAttributesAsync(TbEntityType.DEVICE, TbTestData.GetTestDeviceId(), TbAttributeScope.SERVER_SCOPE, new {testId = 100});
            await client.DeleteEntityAttributesAsync(TbEntityType.DEVICE, TbTestData.GetTestDeviceId(), TbAttributeScope.SERVER_SCOPE, new[] {"testId"});
        });

        // Assert
        Assert.Null(ex);
    }

    [Fact]
    public async Task TestDeleteClientSideEntityAttributes()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();

        // Act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.DeleteEntityAttributesAsync(TbEntityType.DEVICE, TbTestData.GetTestDeviceId(), TbAttributeScope.CLIENT_SCOPE, new[] {"testId"});
        });

        // Assert
        Assert.Null(ex);
    }

    [Fact]
    public async Task TestDeleteNotExistsEntityAttributes()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();

        // Act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.DeleteEntityAttributesAsync(TbEntityType.DEVICE, TbTestData.GetTestCustomerId(), TbAttributeScope.SERVER_SCOPE, new[] {"testId123"});
        });

        // Assert
        Assert.IsType<TbEntityNotFoundException>(ex);
    }

    [Fact]
    public async Task TestDeleteEmptyEntityAttributes()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();

        // Act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.DeleteEntityAttributesAsync(TbEntityType.DEVICE, TbTestData.GetTestDeviceId(), TbAttributeScope.SERVER_SCOPE, Array.Empty<string>());
        });

        // Assert
        Assert.IsType<ArgumentException>(ex);
    }

    [Fact]
    public async Task TestDeleteNullEntityAttributes()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateTelemetryClient();

        // Act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.DeleteEntityAttributesAsync(TbEntityType.DEVICE, TbTestData.GetTestDeviceId(), TbAttributeScope.SERVER_SCOPE, null!);
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
                await client.DeleteEntityAttributesAsync(TbEntityType.DEVICE, Guid.NewGuid(), TbAttributeScope.SERVER_SCOPE, new[] {"testId123"});
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(
            TbTestFactory.Instance.CreateTelemetryClient(),
            async client =>
            {
                await client.DeleteEntityAttributesAsync(TbEntityType.DEVICE, Guid.NewGuid(), TbAttributeScope.SERVER_SCOPE, new[] {"testId123"});
            });
    }
}
