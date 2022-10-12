using System;
using System.Collections.Generic;
using System.Net;

namespace Thingsboard.Net;

/// <summary>
/// The entity of the call to the Thingsboard server
/// </summary>
public class TbClientCall
{
    public string? RequestMethod { get; set; }

    public string? RequestUrl { get; set; }

    public string? RequestQuery { get; set; }

    public Dictionary<string, string>? RequestHeader { get; set; }

    public string? RequestBody { get; set; }

    public HttpStatusCode ResponseStatusCode { get; set; }

    public Dictionary<string, string>? ResponseHeader { get; set; }

    public string? ResponseBody { get; set; }

    public DateTime Time { get; set; }

    public TimeSpan? Elapsed { get; set; }

    public Exception? Exception { get; set; }
}
