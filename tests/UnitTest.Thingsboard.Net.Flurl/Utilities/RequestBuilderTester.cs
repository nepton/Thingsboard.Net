using Thingsboard.Net.Flurl.Options;
using Thingsboard.Net.Flurl.Utilities;

namespace UnitTest.Thingsboard.Net.Flurl.Utilities;

public class RequestBuilderTester
{
    [Fact]
    public void TestMergeCustomOptions()
    {
        // arrange
        var customOptions = new ThingsboardNetFlurlOptions
        {
            BaseUrl            = $"http://{Guid.NewGuid()}:8080",
            Username           = Guid.NewGuid().ToString(),
            Password           = Guid.NewGuid().ToString(),
            TimeoutInSec       = new Random().Next(),
            RetryTimes         = new Random().Next(),
            RetryIntervalInSec = new Random().Next(),
        };
        var builder = TbTestFactory.Instance.CreateRequestBuilder();

        // act
        var newBuilder    = builder.MergeCustomOptions(customOptions);
        var optionsReader = ((FlurlRequestBuilder) newBuilder).OptionsReader;

        // assert
        Assert.Equal(customOptions.BaseUrl,            optionsReader.BaseUrl);
        Assert.Equal(customOptions.Username,           optionsReader.Credentials.Username);
        Assert.Equal(customOptions.Password,           optionsReader.Credentials.Password);
        Assert.Equal(customOptions.TimeoutInSec,       optionsReader.TimeoutInSec);
        Assert.Equal(customOptions.RetryTimes,         optionsReader.RetryTimes);
        Assert.Equal(customOptions.RetryIntervalInSec, optionsReader.RetryIntervalInSec);
    }
}
