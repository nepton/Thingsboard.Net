using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
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

    private readonly ThingsboardNetFlurlOptions _defaultOptions;
    private readonly ITbClientLogger?           _logger;
    private readonly IAccessTokenRepository     _tokenRepository;

    public FlurlRequestBuilder(
        ThingsboardNetFlurlOptions options,
        IAccessTokenRepository     tokenRepository,
        ITbClientLogger?           logger = null)
    {
        _tokenRepository = tokenRepository;
        _logger          = logger;
        _defaultOptions  = options;
    }

    public FlurlRequestBuilder(
        IOptionsSnapshot<ThingsboardNetFlurlOptions> options,
        IAccessTokenRepository                       tokenRepository,
        ITbClientLogger?                             logger = null)
    {
        _tokenRepository = tokenRepository;
        _logger          = logger;
        _defaultOptions  = options.Value;
    }

    public IRequestBuilder MergeCustomOptions(ThingsboardNetFlurlOptions? customOptions)
    {
        var options = _defaultOptions.MergeWith(customOptions);
        return new FlurlRequestBuilder(options, _tokenRepository, _logger);
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
        return new RequestPolicyBuilder(_defaultOptions);
    }

    /// <summary>
    /// Create a new FlurlRequest with a retry policy
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public RequestPolicyBuilder<TResult> GetPolicyBuilder<TResult>()
    {
        return new RequestPolicyBuilder<TResult>(_defaultOptions);
    }

    public async Task<string> GetAccessTokenAsync()
    {
        var options = _defaultOptions;

        var credentials = options.GetCredentials();
        var loginClient = new FlurlTbLoginClient(this);
        var accessToken = await _tokenRepository.GetOrAddTokenAsync(credentials,
            async cancel => await loginClient.LoginAsync(new TbLoginUser(credentials), cancel));

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
        var options = _defaultOptions;

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
                    await _tokenRepository.RemoveExpiredTokenAsync(options.GetCredentials());

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
        if (_logger is not {IsEnabled: true} logger)
            return;

        var tbClientCall = new TbClientCall
        {
            RequestMethod      = call.Request.Verb.Method,
            RequestUrl         = call.Request.Url,
            RequestQuery       = null,
            RequestHeader      = call.Request.Headers.ToDictionary(x => x.Name, x => x.Value),
            RequestBody        = call.RequestBody,
            ResponseStatusCode = call.HttpResponseMessage.StatusCode,
            ResponseHeader     = call.Response.Headers.ToDictionary(x => x.Name, x => x.Value),
            ResponseBody       = await call.Response.GetStringAsync(),
            Time               = DateTime.Now,
            Elapsed            = call.Duration,
            Exception          = call.Exception
        };

        logger.OnLogging(tbClientCall);
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
