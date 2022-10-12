using Microsoft.Extensions.Options;
using Thingsboard.Net.Flurl.Options;

namespace Thingsboard.Net.Flurl.DependencyInjection;

public class OptionsSnapshotReader : IOptionsReader
{
    private readonly ThingsboardNetFlurlOptions _options;

    public OptionsSnapshotReader(IOptionsSnapshot<ThingsboardNetFlurlOptions> options)
    {
        _options = options.Value;
    }

    public ThingsboardNetFlurlOptions GetOptions()
    {
        return _options;
    }
}
