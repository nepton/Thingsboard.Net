using System;
using System.Collections.Generic;
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
using Thingsboard.Net.Flurl.Options;

namespace Thingsboard.Net.Flurl.Utilities.Implements;

public class FlurlRequestBuilder : IRequestBuilder
{
    private static NewtonsoftJsonSerializer? _cached;

    private readonly ThingsboardNetFlurlOptions   _defaultOptions;
    private readonly ILogger<FlurlRequestBuilder> _logger;
    private readonly IServiceProvider             _serviceProvider;

    public FlurlRequestBuilder(
        IOptionsSnapshot<ThingsboardNetFlurlOptions> options,
        ILogger<FlurlRequestBuilder>                 logger,
        IServiceProvider                             serviceProvider)
    {
        _logger          = logger;
        _serviceProvider = serviceProvider;
        _defaultOptions  = options.Value;
    }

    private NewtonsoftJsonSerializer GetCachedNewtonsoftJsonSerializer()
    {
        // Setup for newtonsoft json
        return _cached ??= new NewtonsoftJsonSerializer(
            new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter>()
                {
                    new StringEnumConverter(),
                    new JavaScriptTicksDateTimeConverter() // tb uses javascript ticks for dates
                },
            });
    }

    /// <summary>
    /// Create a new request builder for the specified URL without any authentication
    /// </summary>
    /// <param name="options"></param>
    /// <param name="useAccessToken"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="TbException"></exception>
    /// <exception cref="TbHttpException"></exception>
    public IFlurlRequest CreateRequest(ThingsboardNetFlurlOptions? options, bool useAccessToken)
    {
        // 参数选项
        options = _defaultOptions.MergeWith(options);

        var flurl = GetUrl(options)
            .WithTimeout(TimeSpan.FromSeconds(options.TimeoutInSec ?? _defaultOptions.TimeoutInSec ?? 10))
            .ConfigureRequest(action =>
            {
                // Setup for newtonsoft json
                action.JsonSerializer = GetCachedNewtonsoftJsonSerializer();
            })
            .BeforeCall(async call =>
            {
                if (useAccessToken)
                {
                    // WARN: accessTokenService should use ITbLogin interface, so we should avoid recursive reference
                    var accessTokenService = _serviceProvider.GetRequiredService<IAccessToken>();
                    var credentials        = options.GetCredentials();
                    var accessToken        = await accessTokenService.GetAccessTokenAsync(credentials);
                    call.Request.WithHeader("Authorization", $"Bearer {accessToken}");
                }
            })
            .AfterCall(async call =>
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
                    _logger.LogInformation(call.Exception,
                        "HTTP {Method}: {Url} returned {StatusCode} cost {Elapsed}",
                        call.Request.Verb,
                        call.Request.Url,
                        call.Response.StatusCode,
                        call.Duration?.TotalMilliseconds);
                }

                if (call.Response.StatusCode >= 400)
                {
                    var statusCode = (HttpStatusCode) call.Response.StatusCode;
                    var text       = await call.Response.GetStringAsync();

                    if (string.IsNullOrEmpty(text))
                        throw new TbHttpException($"StatusCode is {statusCode}, no response body", statusCode, DateTime.Now, -1);

                    try
                    {
                        var error = JsonConvert.DeserializeObject<TbErrorResponse>(text);
                        throw new TbHttpException(error.Message ?? "", (HttpStatusCode) error.Status, error.Timestamp, error.ErrorCode);
                    }
                    catch
                    {
                        // BE CAREFUL, sometimes (like 400 of saveEntityAttributes) the response body is not a json string
                        throw new TbHttpException(text, statusCode, DateTime.Now, -1);
                    }
                }
            })
            .OnError(async call =>
            {
                _logger.LogWarning(call.Exception,
                    "HTTP {Method}: {Url} returned {StatusCode} cost {Elapsed}\n" +
                    "request body {RequestBody}\n" +
                    "response body {ResponseBody}",
                    call.Request.Verb,
                    call.Request.Url,
                    call.Response.StatusCode,
                    call.Duration?.TotalMilliseconds,
                    call.RequestBody,
                    await call.Response.GetStringAsync());
            });

        return flurl;
    }

    /// <summary>
    /// Create a new FlurlRequest with a retry policy
    /// </summary>
    /// <returns></returns>
    public RequestPolicyBuilder GetPolicyBuilder(ThingsboardNetFlurlOptions? customOptions)
    {
        if (customOptions == null) throw new ArgumentNullException(nameof(customOptions));

        // Merge parameter options
        customOptions = _defaultOptions.MergeWith(customOptions);

        return new RequestPolicyBuilder(customOptions);
    }

    /// <summary>
    /// Create a new FlurlRequest with a retry policy
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public RequestPolicyBuilder<TResult> GetPolicyBuilder<TResult>(ThingsboardNetFlurlOptions? customOptions)
    {
        if (customOptions == null) throw new ArgumentNullException(nameof(customOptions));

        // Merge parameter options
        customOptions = _defaultOptions.MergeWith(customOptions);

        return new RequestPolicyBuilder<TResult>(customOptions);
    }

    /// <summary>
    /// 获取 URL
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    private Url GetUrl(ThingsboardNetFlurlOptions options)
    {
        if (options == null) throw new ArgumentNullException(nameof(options));

        if (string.IsNullOrEmpty(options.BaseUrl))
            throw new ArgumentNullException(nameof(Url), "Thingsboard URL is not set");

        var url = new Url(options.BaseUrl);

        if (url.Scheme != "http" && url.Scheme != "https")
            throw new ArgumentException("Thingsboard URL must be http or https", nameof(Url));

        if (url.Host == null)
            throw new ArgumentException("Thingsboard URL must be contains host", nameof(Url));

        return url;
    }
}
