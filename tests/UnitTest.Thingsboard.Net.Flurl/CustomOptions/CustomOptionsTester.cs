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
    [Theory]
    [InlineData("wrongUser", "wrongPassword")]
    [InlineData("",          "wrongPassword")]
    [InlineData(null,        "wrongPassword")]
    [InlineData("wrongUser", "")]
    [InlineData("wrongUser", null)]
    [InlineData("",          "")]
    [InlineData(null,        null)]
    public void TestWithCredentials(string? username, string password)
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateAuthClient();

        // Act
        if (username == null)
        {
            Assert.Throws<ArgumentNullException>(() => client.WithCredentials(username!, password));
            return;
        }

        client.WithCredentials(username, password);
        var options = client is IUnitTestOptionsReader reader ? reader.GetOptions() : null;

        // Assert
        Assert.NotNull(options);
        Assert.Equal(username, options!.Username);
        Assert.Equal(password, options!.Password);
    }

    [Theory]
    [InlineData("http://localhost:8080")]
    [InlineData("https://localhost:8080/")]
    [InlineData("http://localhost:8080/api")]
    [InlineData("https://localhost:8080/api/")]
    [InlineData("http://12.34.56.78:8080/api/v1")]
    [InlineData("https://12.34.56.78:8080/api/v1")]
    [InlineData("http://abcd.efgh.com:8080/api")]
    [InlineData("https://abcd.efgh.com:8080/api")]
    public void TestWithBaseUrl(string baseUrl)
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateAuthClient();

        // Act
        client.WithBaseUrl(baseUrl);
        var options = client is IUnitTestOptionsReader reader ? reader.GetOptions() : null;

        // Assert
        Assert.NotNull(options);
        Assert.Equal(baseUrl, options!.BaseUrl);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("wrongUrl:1234/")]
    [InlineData("htt://wrongUrl:1234/")]
    [InlineData("http://wrongUrl:abcd/")]
    [InlineData("https://wrongUrl:123456/")]
    public void TestEdgeValueWithBaseUrl(string? baseUrl)
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateAuthClient();

        // Act
        Assert.ThrowsAny<Exception>(() => client.WithBaseUrl(baseUrl!));
    }

    [Fact]
    public void TestWithHttpTimeout()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateAuthClient();

        // Act
        var customHttpTimeout   = new Random().Next(1000);
        var customRetryCount    = new Random().Next(1000);
        var customRetryInterval = new Random().Next(1000);
        client.WithHttpTimeout(customHttpTimeout, customRetryCount, customRetryInterval);
        var options = client is IUnitTestOptionsReader reader ? reader.GetOptions() : null;

        // Assert
        Assert.NotNull(options);
        Assert.Equal(customHttpTimeout,   options!.TimeoutInSec);
        Assert.Equal(customRetryCount,    options!.RetryTimes);
        Assert.Equal(customRetryInterval, options!.RetryIntervalInSec);
    }

    [Fact]
    public void TestEdgeValueWithHttpTimeout()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateAuthClient();

        // Act
        var customHttpTimeout   = -new Random().Next(1000);
        var customRetryCount    = -new Random().Next(1000);
        var customRetryInterval = -new Random().Next(1000);
        Assert.ThrowsAny<ArgumentException>(() => client.WithHttpTimeout(customHttpTimeout, customRetryCount, customRetryInterval));
    }
}
