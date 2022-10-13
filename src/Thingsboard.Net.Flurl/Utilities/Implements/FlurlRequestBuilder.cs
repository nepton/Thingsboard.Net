using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
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

    private readonly ILoggerFactory                   _loggerFactory;
    private readonly ILogger<FlurlRequestBuilder>     _logger;
    private readonly ThingsboardNetFlurlOptionsReader _optionsReader;
    private readonly IAccessTokenRepository           _tokenRepository;

    public FlurlRequestBuilder(
        IOptionsReaderFactory  optionsReaderFactory,
        IAccessTokenRepository tokenRepository,
        ILoggerFactory         loggerFactory)
    {
        _optionsReader   = optionsReaderFactory.GetOptionsReader();
        _tokenRepository = tokenRepository;
        _loggerFactory   = loggerFactory;
        _logger          = loggerFactory.CreateLogger<FlurlRequestBuilder>();
    }

    public ThingsboardNetFlurlOptionsReader OptionsReader => _optionsReader;

    public IRequestBuilder MergeCustomOptions(ThingsboardNetFlurlOptions? customOptions)
    {
        var optionsReader = new ThingsboardNetFlurlOptionsReader(_optionsReader.Options);
        if (customOptions != null)
            optionsReader.AddHighPriorityOptions(customOptions);

        return new FlurlRequestBuilder(
            new DefaultOptionsReaderFactory(optionsReader),
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
        return new RequestPolicyBuilder(_optionsReader, logger);
    }

    /// <summary>
    /// Create a new FlurlRequest with a retry policy
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public RequestPolicyBuilder<TResult> GetPolicyBuilder<TResult>()
    {
        var logger = _loggerFactory.CreateLogger<RequestPolicyBuilder<TResult>>();
        return new RequestPolicyBuilder<TResult>(_optionsReader, logger);
    }

    public async Task<string> GetAccessTokenAsync()
    {
        var options = _optionsReader;

        var credentials = options.Credentials;
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
        var options = _optionsReader;

        var flurl = options.BaseUrl
            .WithTimeout(TimeSpan.FromSeconds(options.TimeoutInSec))
            .ConfigureRequest(action =>
            {
                // Setup for newtonsoft json
                action.JsonSerializer = GetCachedNewtonsoftJsonSerializer();
            })
            .AfterCall(async call =>
            {
                // Clear the access token if the request got 401
                if (call.Completed && call.Response.StatusCode == 401 && call.Request.Headers.Contains("Authorization"))
                {
                    _logger.LogDebug("Access token is invalid, clearing it");
                    await _tokenRepository.RemoveExpiredTokenAsync(options.Credentials);
                }

                // Log the request
                await LogCalledAsync(call);

                // Throw exception if the request got 4xx or 5xx
                if (await GenerateExceptionAsync(call) is { } ex)
                    throw ex;
            });

        return flurl;
    }

    private static async Task<Exception?> GenerateExceptionAsync(FlurlCall call)
    {
        if (call.Response == null)
        {
            if (call.Exception is { } ex)
                return new TbHttpException(ex.Message, call.Completed, null, DateTime.Now, 0);

            return new TbHttpException("Unknown error", call.Completed, null, DateTime.Now, 0);
        }

        if (call.Response.StatusCode >= 400)
        {
            var statusCode = (HttpStatusCode) call.Response.StatusCode;
            var text       = await call.Response.GetStringAsync();

            if (string.IsNullOrEmpty(text))
                return new TbHttpException($"StatusCode is {statusCode}, no response body", call.Completed, statusCode, DateTime.Now, -1);

            var error = text.TryDeserializeObject<TbErrorResponse>();
            if (error != null)
                return new TbHttpException(error.Message ?? "", call.Completed, (HttpStatusCode) error.Status, error.Timestamp, error.ErrorCode);

            // BE CAREFUL, sometimes (like 400 of saveEntityAttributes) the response body is not a json string
            return new TbHttpException(text, call.Completed, statusCode, DateTime.Now, -1);
        }

        return null;
    }

    private async Task LogCalledAsync(FlurlCall call)
    {
        var level = call.Completed ? call.Response.StatusCode >= 400 ? LogLevel.Warning : LogLevel.Information : LogLevel.Error;

        if (!_logger.IsEnabled(level))
            return;

        var state = new Dictionary<string, object>
        {
            ["RequestBody"]    = call.RequestBody,
            ["RequestHeaders"] = call.Request.Headers,
        };
        if (call.Response is { } response)
        {
            state["ResponseBody"]    = await response.GetStringAsync();
            state["ResponseHeaders"] = response.Headers;
        }

        using var _ = _logger.BeginScope(state);

        _logger.Log(level,
            call.Exception,
            "HTTP {Method}: {Url} returned {StatusCode} cost {Elapsed}",
            call.Request.Verb,
            call.Request.Url,
            call.Response?.StatusCode,
            call.Duration?.TotalMilliseconds);
    }
}
