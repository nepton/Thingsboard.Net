using System;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using Thingsboard.Net.DependencyInjection;
using Thingsboard.Net.Exceptions;

namespace Thingsboard.Net.Utility;

public class FlurlRequestBuilder : IRequestBuilder
{
    private readonly ThingsboardNetOptions _options;

    public FlurlRequestBuilder(IOptionsSnapshot<ThingsboardNetOptions> options)
    {
        _options = options.Value;
    }

    public IFlurlRequest CreateRequest(string path)
    {
        if (path == null) throw new ArgumentNullException(nameof(path));
        var baseUrl = _options.Url ?? throw new TbException("Thingsboard URL is not set");
        var url     = baseUrl.AppendPathSegment(path);

        return new FlurlRequest(url)
            .WithTimeout(_options.GetTimeoutInSecOrDefault());
    }
}
