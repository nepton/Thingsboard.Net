using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Thingsboard.Net.Exceptions;
using Thingsboard.Net.Flurl.Options;
using Thingsboard.Net.Flurl.Utilities.Implements;

namespace Thingsboard.Net.Flurl.Utilities;

public class FlurlRequestBuilder : IRequestBuilder
{
    private static NewtonsoftJsonSerializer? _cached;

    private readonly ILoggerFactory               _loggerFactory;
    private readonly ILogger<FlurlRequestBuilder> _logger;
    private readonly IOptionsReader               _optionsReader;
    private readonly IAccessTokenRepository       _tokenRepository;

    public FlurlRequestBuilder(
        IOptionsReader         optionsReader,
        IAccessTokenRepository tokenRepository,
        ILoggerFactory         loggerFactory)
    {
        _optionsReader   = optionsReader;
        _tokenRepository = tokenRepository;
        _loggerFactory   = loggerFactory;
        _logger          = loggerFactory.CreateLogger<FlurlRequestBuilder>();
    }

    public IRequestBuilder MergeCustomOptions(ThingsboardNetFlurlOptions? customOptions)
    {
        var options = _optionsReader.GetOptions().MergeWith(customOptions);
        return new FlurlRequestBuilder(
            new OptionsReader(options),
            _tokenRepository,
            _loggerFactory);
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
    /// Create a new FlurlRequest with a retry policy
    /// </summary>
    /// <returns></returns>
    public RequestPolicyBuilder GetPolicyBuilder()
    {
        var logger = _loggerFactory.CreateLogger<RequestPolicyBuilder>();
        return new RequestPolicyBuilder(_optionsReader.GetOptions(), logger);
    }

    /// <summary>
    /// Create a new FlurlRequest with a retry policy
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public RequestPolicyBuilder<TResult> GetPolicyBuilder<TResult>()
    {
        var logger = _loggerFactory.CreateLogger<RequestPolicyBuilder<TResult>>();
        return new RequestPolicyBuilder<TResult>(_optionsReader.GetOptions(), logger);
    }

    public async Task<string> GetAccessTokenAsync()
    {
        var options = _optionsReader.GetOptions();

        var credentials = options.GetCredentials();
        var loginClient = new FlurlTbLoginClient(this);
        var accessToken = await _tokenRepository.GetOrAddTokenAsync(credentials,
            async cancel =>
            {
                _logger.LogDebug("Getting access token from Thingsboard");
                return await loginClient.LoginAsync(new TbLoginUser(credentials), cancel);
            });

        return accessToken;
    }

    /// <summary>
    /// Create a new request builder for the specified URL without any authentication
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="TbException"></exception>
    /// <exception cref="TbHttpException"></exception>
    public IFlurlRequest CreateRequest()
    {
        // 参数选项
        var options = _optionsReader.GetOptions();

        var flurl = GetBaseUrl(options)
            .WithTimeout(TimeSpan.FromSeconds(options.TimeoutInSec ?? 10))
            .ConfigureRequest(action =>
            {
                // Setup for newtonsoft json
                action.JsonSerializer = GetCachedNewtonsoftJsonSerializer();
            })
            .AfterCall(async call =>
            {
                // Clear the access token if the request got 401
                if (call.Response.StatusCode == 401 && call.Request.Headers.Contains("Authorization"))
                {
                    _logger.LogDebug("Access token is invalid, clearing it");
                    await _tokenRepository.RemoveExpiredTokenAsync(options.GetCredentials());
                }

                // Log the request
                await LogCalledAsync(call);

                // Throw exception if the request got 4xx or 5xx
                await ThrowIfBadRequest(call);
            });

        return flurl;
    }

    private static async Task ThrowIfBadRequest(FlurlCall call)
    {
        if (call.Response.StatusCode >= 400)
        {
            var statusCode = (HttpStatusCode) call.Response.StatusCode;
            var text       = await call.Response.GetStringAsync();

            if (string.IsNullOrEmpty(text))
                throw new TbHttpException($"StatusCode is {statusCode}, no response body", statusCode, DateTime.Now, -1);

            var error = text.TryDeserializeObject<TbErrorResponse>();
            if (error != null)
                throw new TbHttpException(error.Message ?? "", (HttpStatusCode) error.Status, error.Timestamp, error.ErrorCode);

            // BE CAREFUL, sometimes (like 400 of saveEntityAttributes) the response body is not a json string
            throw new TbHttpException(text, statusCode, DateTime.Now, -1);
        }
    }

    private async Task LogCalledAsync(FlurlCall call)
    {
        var level = call.Response.StatusCode >= 400 ? LogLevel.Warning : LogLevel.Information;

        if (!_logger.IsEnabled(level))
            return;

        var responseBody = await call.Response.GetStringAsync();
        using var _ = _logger.BeginScope(new Dictionary<string, object>
        {
            ["RequestBody"]     = call.RequestBody,
            ["ResponseBody"]    = responseBody,
            ["RequestHeaders"]  = call.Request.Headers,
            ["ResponseHeaders"] = call.Response.Headers,
        });

        _logger.Log(level,
            call.Exception,
            "HTTP {Method}: {Url} returned {StatusCode} cost {Elapsed}",
            call.Request.Verb,
            call.Request.Url,
            call.Response.StatusCode,
            call.Duration?.TotalMilliseconds);
    }

    /// <summary>
    /// 获取 URL
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    private Url GetBaseUrl(ThingsboardNetFlurlOptions options)
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
