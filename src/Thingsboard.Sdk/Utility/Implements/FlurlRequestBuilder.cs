using System;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using Thingsboard.Sdk.DependencyInjection;
using Thingsboard.Sdk.Exceptions;

namespace Thingsboard.Sdk.Utility;

public class FlurlRequestBuilder : IRequestBuilder
{
    private readonly ThingsboardSdkOptions _options;

    public FlurlRequestBuilder(IOptionsSnapshot<ThingsboardSdkOptions> options)
    {
        _options = options.Value;
    }

    public IFlurlRequest CreateRequest(string path)
    {
        if (path == null) throw new ArgumentNullException(nameof(path));
        var baseUrl = _options.Url ?? throw new TbException("Thingsboard URL is not set");
        var url     = baseUrl.AppendPathSegment(path);

        return new FlurlRequest(url)
            .WithTimeout(_options.TimeoutInSec);
    }
}
