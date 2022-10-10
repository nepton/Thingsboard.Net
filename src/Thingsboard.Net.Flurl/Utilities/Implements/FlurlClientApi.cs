using System;
using Thingsboard.Net.Flurl.Options;

namespace Thingsboard.Net.Flurl.Utilities.Implements;

public abstract class FlurlClientApi<TClientApi> : ITbClient<TClientApi> where TClientApi : ITbClient<TClientApi>
{
    private readonly ThingsboardNetFlurlOptions _options = new();

    protected ThingsboardNetFlurlOptions GetCustomOptions()
    {
        return _options;
    }

    public TClientApi WithCredentials(string username, string? password)
    {
        if (username == null) throw new ArgumentNullException(nameof(username));

        _options.Username = username;
        _options.Password = password;

        return (TClientApi) (object) this;
    }
}
