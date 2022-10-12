using Thingsboard.Net.Flurl;

namespace UnitTest.Thingsboard.Net.Flurl;

public class TbTestFactory
{
    public static FlurlTbClientFactory Instance { get; } = new()
    {
        Options = new()
        {
            BaseUrl            = "http://localhost:8080",
            Username           = "tenant@thingsboard.org",
            Password           = "tenant",
            TimeoutInSec       = 10,
            RetryTimes         = 3,
            RetryIntervalInSec = 1,
        },
    };
}
