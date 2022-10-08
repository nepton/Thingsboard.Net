using System;
using Thingsboard.Net.Common;
using Thingsboard.Net.Options;
using Thingsboard.Net.Utility;

namespace Thingsboard.Net.Flurl.Utility.Implements;

public abstract class FlurlClientApi<TClientApi> : IClientApi<TClientApi> where TClientApi : IClientApi<TClientApi>
{
    private readonly ThingsboardNetOptions _options = new();

    public TClientApi WithCredentials(TbCredentials credentials)
    {
        if (credentials == null) throw new ArgumentNullException(nameof(credentials));

        _options.Username = credentials.Username;
        _options.Password = credentials.Password;

        return (TClientApi) (object) this;
    }

    protected ThingsboardNetOptions GetCustomOptions()
    {
        return _options;
    }
}
