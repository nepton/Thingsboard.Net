using Thingsboard.Net.Exceptions;
using Thingsboard.Net.Flurl.Options;

namespace UnitTest.Thingsboard.Net.Flurl.Options;

/// <summary>
/// Unit test for ThingsboardNetFlurlOptionsReader
/// We will following scenarios:
/// 1: default value
/// 2: add 1 or more options and make some null value options in the top
/// </summary>
public class ThingsboardNetFlurlOptionsReaderTester
{
    [Fact]
    public void TestCheckBaseUrl()
    {
        var schemas = new[] {"http://", "https://"};
        var hosts   = new[] {"localhost", "server.com", "tb.product.company.com", "10.0.0.1", "[::1]", "[2001:db8::1]"};
        var ports   = new[] {"", ":8080", ":80"};
        var paths   = new[] {"", "/", "/api", "/api/v1", "/api/v1/"};

        var urls = from schema in schemas
                   from host in hosts
                   from port in ports
                   from path in paths
                   select $"{schema}{host}{port}{path}";

        foreach (var url in urls)
        {
            var options = new ThingsboardNetFlurlOptions();
            options.BaseUrl = url;
            var reader = new ThingsboardNetFlurlOptionsReader(options);

            var actualBaseUrl = reader.BaseUrl;
            Assert.Equal(url, actualBaseUrl);
        }
    }

    /// <summary>
    /// Read default value when null
    /// </summary>
    [Fact]
    public void TestNullBaseUrl()
    {
        var options = new ThingsboardNetFlurlOptions();
        options.BaseUrl = null;
        var reader = new ThingsboardNetFlurlOptionsReader(options);

        var ex = Record.Exception(() => reader.BaseUrl);
        Assert.Null(ex);
    }

    [Theory]
    [InlineData("localhost:8080/api/v1")]
    [InlineData("127.0.0.1:8080/api/v1")]
    [InlineData("[::1]:8080/api/v1")]
    [InlineData("localhost/api/v1")]
    [InlineData("localhost:8080")]
    [InlineData("localhost")]
    [InlineData("10.0.0.1")]
    [InlineData("[::1]")]
    [InlineData("")]
    public void TestInvalidBaseUrl(string? url)
    {
        var options = new ThingsboardNetFlurlOptions();
        options.BaseUrl = url;
        var reader = new ThingsboardNetFlurlOptionsReader(options);

        Assert.Throws<TbOptionsException>(() => reader.BaseUrl);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(0, 1)]
    [InlineData(0, 2)]
    [InlineData(1, 2)]
    [InlineData(2, 2)]
    [InlineData(3, 2)]
    [InlineData(1, 0)]
    [InlineData(2, 0)]
    [InlineData(3, 0)]
    public void TestMultiLayersOptions(int effectLayers, int nullLayers)
    {
        // arrange
        var reader = new ThingsboardNetFlurlOptionsReader();

        // act
        var lastOptions = ThingsboardNetFlurlOptionsReader.DefaultOptions;
        for (int i = 0; i < effectLayers; i++)
        {
            lastOptions = new ThingsboardNetFlurlOptions
            {
                BaseUrl            = $"http://{Guid.NewGuid()}:8080",
                Username           = Guid.NewGuid().ToString(),
                Password           = Guid.NewGuid().ToString(),
                TimeoutInSec       = new Random().Next(),
                RetryTimes         = new Random().Next(),
                RetryIntervalInSec = new Random().Next(),
            };
            reader.AddHighPriorityOptions(lastOptions);
        }

        for (int i = 0; i < nullLayers; i++)
        {
            reader.AddHighPriorityOptions(new ThingsboardNetFlurlOptions());
        }

        // assert
        Assert.Equal(lastOptions.BaseUrl,            reader.BaseUrl);
        Assert.Equal(lastOptions.Username,           reader.Credentials.Username);
        Assert.Equal(lastOptions.Password,           reader.Credentials.Password);
        Assert.Equal(lastOptions.TimeoutInSec,       reader.TimeoutInSec);
        Assert.Equal(lastOptions.RetryTimes,         reader.RetryTimes);
        Assert.Equal(lastOptions.RetryIntervalInSec, reader.RetryIntervalInSec);
    }
}
