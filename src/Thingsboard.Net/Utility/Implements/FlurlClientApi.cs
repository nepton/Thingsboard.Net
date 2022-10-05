namespace Thingsboard.Net.Utility;

public abstract class FlurlClientApi<TClientApi> : IClientApi<TClientApi> where TClientApi : IClientApi<TClientApi>
{
    private readonly TbClientApiOptions _options = new();

    public TClientApi WithCredentials(TbCredentials credentials)
    {
        _options.Credentials = credentials;
        return (TClientApi) (object) this;
    }

    protected TbClientApiOptions GetOptions()
    {
        return _options;
    }
}
