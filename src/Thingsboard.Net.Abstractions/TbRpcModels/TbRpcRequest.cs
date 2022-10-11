namespace Thingsboard.Net;

public class TbRpcRequest
{
    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public TbRpcRequest(string method)
    {
        Method = method;
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public TbRpcRequest(string method, object? @params)
    {
        Method = method;
        Params = @params;
    }

    /// <summary>
    /// method - mandatory, name of the method to distinct the RPC calls. For example, "getCurrentTime" or "getWeatherForecast". The value of the parameter is a string.
    /// </summary>
    public string Method { get; set; }

    /// <summary>
    /// params - mandatory, parameters used for processing of the request. The value is a JSON. Leave empty JSON "{}" if no parameters needed.
    /// </summary>
    public object? Params { get; set; }

    /// <summary>
    /// timeout - optional, value of the processing timeout in milliseconds. The default value is 10000 (10 seconds). The minimum value is 5000 (5 seconds).
    /// </summary>
    public int? Timeout { get; set; }

    /// <summary>
    /// expirationTime - optional, value of the epoch time (in milliseconds, UTC timezone). Overrides timeout if present.
    /// </summary>
    public int? ExpirationTime { get; set; }

    /// <summary>
    /// persistent - optional, indicates persistent RPC. The default value is "false".
    /// </summary>
    public bool? Persistent { get; set; }

    /// <summary>
    /// retries - optional, defines how many times persistent RPC will be re-sent in case of failures on the network and/or device side.
    /// </summary>
    public int? Retries { get; set; }

    /// <summary>
    /// optional, defines metadata for the persistent RPC that will be added to the persistent RPC events.
    /// </summary>
    public object? AdditionalInfo { get; set; }
}
