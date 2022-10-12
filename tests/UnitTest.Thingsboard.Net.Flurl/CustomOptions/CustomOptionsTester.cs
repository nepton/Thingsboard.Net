using Thingsboard.Net.Flurl.Utilities;

namespace UnitTest.Thingsboard.Net.Flurl.CustomOptions;

/// <summary>
/// This class is used to test the custom options for the thingsboard.net flurl client.
/// A client can be custom options before send the request to the thingsboard server.
/// For example: var userInfo = await client.WithCredentials("wrongUser", "wrongPassword").GetCurrentUserAsync();
/// We provide the custom options are all declared in ITbClient{out TClient} interface.
///
/// This class will test all methods of the ITbClient{out TClient} interface.
///
/// 1. WithCredentials
/// 2. WithBaseUrl
/// 3. WithHttpTimeout 
/// </summary>
public class CustomOptionsTester
{
    [Fact]
    public void TestWithCredentials()
    {
        // Arrange
        var client         = TbTestFactory.Instance.CreateAuthClient();
        var defaultOptions = TbTestFactory.Instance.Options;

        // Act
        var wrongUsername = "wrongUsername";
        var wrongPassword = "wrongPassword";
        client.WithCredentials(wrongUsername, wrongPassword);
        var options = client is IUnitTestOptionsReader reader ? reader.GetOptions() : null;

        // Assert
        Assert.NotNull(options);
        Assert.Equal(wrongUsername, options!.Username);
        Assert.Equal(wrongPassword, options!.Password);

        Assert.NotEqual(defaultOptions.Username, options!.Username);
        Assert.NotEqual(defaultOptions.Password, options!.Password);
    }
    
    
}


