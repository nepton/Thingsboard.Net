namespace Thingsboard.Net.Flurl.Options;

internal class OptionsReader : IOptionsReader
{
    private readonly ThingsboardNetFlurlOptions _options;

    public OptionsReader(ThingsboardNetFlurlOptions options)
    {
        _options = options;
    }

    public ThingsboardNetFlurlOptions GetOptions()
    {
        return _options;
    }
}
