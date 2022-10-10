using System;
using Thingsboard.Net.Flurl.Options;

namespace Thingsboard.Net.Flurl.Utilities.Implements;

public abstract class FlurlTbClient<TClient> : ITbClient<TClient> where TClient : ITbClient<TClient>
{
    private readonly ThingsboardNetFlurlOptions _options = new();

    protected ThingsboardNetFlurlOptions GetCustomOptions()
    {
        return _options;
    }

    public TClient WithCredentials(string username, string? password)
    {
        if (username == null) throw new ArgumentNullException(nameof(username));

        _options.Username = username;
        _options.Password = password;

        return (TClient) (object) this;
    }
}
