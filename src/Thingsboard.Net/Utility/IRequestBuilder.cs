using Flurl.Http;

namespace Thingsboard.Net.Utility;

public interface IRequestBuilder
{
    IFlurlRequest CreateRequest(string path);
}
