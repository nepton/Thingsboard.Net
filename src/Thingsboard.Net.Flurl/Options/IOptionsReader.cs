namespace Thingsboard.Net.Flurl.Options;

/// <summary>
/// The global default options for all <see cref="ITbClient{TClient}"/> instances.
/// </summary>
public interface IOptionsReader
{
    ThingsboardNetFlurlOptions GetOptions();
}