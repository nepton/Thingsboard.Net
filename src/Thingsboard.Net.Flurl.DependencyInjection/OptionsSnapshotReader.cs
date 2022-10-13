using Microsoft.Extensions.Options;
using Thingsboard.Net.Flurl.Options;

namespace Thingsboard.Net.Flurl.DependencyInjection;

public class OptionsSnapshotReader : IOptionsReaderFactory
{
    private readonly ThingsboardNetFlurlOptions _options;

    public OptionsSnapshotReader(IOptionsSnapshot<ThingsboardNetFlurlOptions> options)
    {
        _options = options.Value;
    }

    public ThingsboardNetFlurlOptionsReader GetOptionsReader()
    {
        return new ThingsboardNetFlurlOptionsReader(_options);
    }
}
