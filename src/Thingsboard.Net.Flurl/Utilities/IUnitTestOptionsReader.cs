using Thingsboard.Net.Flurl.Options;

namespace Thingsboard.Net.Flurl.Utilities;

public interface IUnitTestOptionsReader
{
    ThingsboardNetFlurlOptions? GetOptions();
}
