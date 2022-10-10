using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Net;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Thingsboard.Net.Exceptions;
using Thingsboard.Net.Options;

namespace Thingsboard.Net.Flurl.Utility.Implements;

public class FlurlRequestBuilder : IRequestBuilder
{
    private readonly ThingsboardNetOptions        _defaultOptions;
    private readonly ILogger<FlurlRequestBuilder> _logger;
    private readonly IServiceProvider             _serviceProvider;

    public FlurlRequestBuilder(
        IOptionsSnapshot<ThingsboardNetOptions> options,
        ILogger<FlurlRequestBuilder>            logger,
        IServiceProvider                        serviceProvider)
    {
        _logger          = logger;
        _serviceProvider = serviceProvider;
        _defaultOptions  = options.Value;
    }

    /// <summary>
    /// Create a new request builder for the specified URL without any authentication
    /// </summary>
    /// <param name="path"></param>
    /// <param name="options"></param>
    /// <param name="useAccessToken"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="TbException"></exception>
    /// <exception cref="TbHttpException"></exception>
    public IFlurlRequest CreateRequest(string path, ThingsboardNetOptions options, bool useAccessToken)
    {
        if (path == null) throw new ArgumentNullException(nameof(path));

        // 参数选项
        options = _defaultOptions.MergeWith(options);

        var flurl = GetUrl(options)
            .AppendPathSegment(path)
            .WithTimeout(TimeSpan.FromSeconds(options.TimeoutInSec ?? _defaultOptions.TimeoutInSec ?? 10))
            .ConfigureRequest(action =>
            {
                // Setup for newtonsoft json
                var settings = new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                };
                settings.Converters.Add(new StringEnumConverter());
                settings.Converters.Add(new JavaScriptTicksDateTimeConverter()); // tb uses javascript ticks for dates

                action.JsonSerializer = new NewtonsoftJsonSerializer(settings);

                action.BeforeCallAsync = async (call) =>
                {
                    if (useAccessToken)
                    {
                        // WARN: accessTokenService should use ITbLogin interface, so we should avoid recursive reference
                        var accessTokenService = _serviceProvider.GetRequiredService<IAccessToken>();
                        var credentials        = options.GetCredentials();
                        var accessToken        = await accessTokenService.GetAccessTokenAsync(credentials);
                        call.Request.WithHeader("Authorization", $"Bearer {accessToken}");
                    }
                };

                action.AfterCallAsync = async (call) =>
                {
                    // Clear the access token if the request got 401
                    if (call.Response.StatusCode == 401 && useAccessToken)
                    {
                        // WARN: accessTokenService should use ITbLogin interface, so we should avoid recursive reference
                        var accessTokenService = _serviceProvider.GetRequiredService<IAccessToken>();
                        var credentials        = options.GetCredentials();
                        await accessTokenService.RemoveExpiredTokenAsync(credentials);
                    }

                    if (_logger.IsEnabled(LogLevel.Debug))
                    {
                        var responseBody = await call.Response.GetStringAsync();
                        using var _ = _logger.BeginScope(new Dictionary<string, object>
                        {
                            ["RequestBody"]     = call.RequestBody,
                            ["ResponseBody"]    = responseBody,
                            ["RequestHeaders"]  = call.Request.Headers,
                            ["ResponseHeaders"] = call.Response.Headers,
                        });
                        _logger.LogDebug(call.Exception,
                            "HTTP {Method}: {Url} returned {StatusCode} cost {Elapsed}",
                            call.Request.Verb,
                            call.Request.Url,
                            call.Response.StatusCode,
                            call.Duration?.TotalMilliseconds);
                    }

                    if (call.Response.StatusCode >= 400)
                    {
                        var error = await call.Response.GetJsonAsync<TbErrorResponse>();
                        throw new TbHttpException(error.Message ?? "", (HttpStatusCode) error.Status, error.Timestamp, error.ErrorCode);
                    }
                };
            });

        return flurl;
    }

    /// <summary>
    /// Create a new FlurlRequest with a retry policy
    /// </summary>
    /// <returns></returns>
    public RequestPolicyBuilder GetDefaultPolicy()
    {
        return new RequestPolicyBuilder().RetryOnTimeout(3, 1);
    }

    /// <summary>
    /// Create a new FlurlRequest with a retry policy
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public RequestPolicyBuilder<TResult> GetDefaultPolicy<TResult>()
    {
        return new RequestPolicyBuilder<TResult>().RetryOnTimeout(3, 1);
    }

    /// <summary>
    /// 获取 URL
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    private Url GetUrl(ThingsboardNetOptions options)
    {
        if (options == null) throw new ArgumentNullException(nameof(options));

        if (string.IsNullOrEmpty(options.Url))
            throw new ArgumentNullException(nameof(Url), "Thingsboard URL is not set");

        var url = new Url(options.Url);

        if (url.Scheme != "http" && url.Scheme != "https")
            throw new ArgumentException("Thingsboard URL must be http or https", nameof(Url));

        if (url.Host == null)
            throw new ArgumentException("Thingsboard URL must be contains host", nameof(Url));

        return url;
    }
}
