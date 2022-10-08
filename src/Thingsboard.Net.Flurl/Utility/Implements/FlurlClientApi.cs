using System;
using Thingsboard.Net.Common;
using Thingsboard.Net.Options;

namespace Thingsboard.Net.Flurl.Utility.Implements;

public abstract class FlurlClientApi<TClientApi> : IClientApi<TClientApi> where TClientApi : IClientApi<TClientApi>
{
    private readonly ThingsboardNetOptions _options = new();

    protected ThingsboardNetOptions GetCustomOptions()
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
