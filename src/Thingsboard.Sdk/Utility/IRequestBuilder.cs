using Flurl.Http;

namespace Thingsboard.Sdk.Utility;

public interface IRequestBuilder
{
    IFlurlRequest CreateRequest(string path);
}
