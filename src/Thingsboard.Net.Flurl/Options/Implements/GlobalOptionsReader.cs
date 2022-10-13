namespace Thingsboard.Net.Flurl.Options;

internal class DefaultOptionsReaderFactory : IOptionsReaderFactory
{
    private readonly ThingsboardNetFlurlOptionsReader _reader;

    public DefaultOptionsReaderFactory(ThingsboardNetFlurlOptionsReader reader)
    {
        _reader = reader;
    }

    public ThingsboardNetFlurlOptionsReader GetOptionsReader()
    {
        return _reader;
    }
}
