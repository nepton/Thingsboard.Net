using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Thingsboard.Net.Flurl.Utilities.Implements;

public class TbErrorResponse
{
    public int     Status    { get; set; }
    public string? Message   { get; set; }
    public int     ErrorCode { get; set; }

    [JsonConverter(typeof(IsoDateTimeConverter))] // this is the standard format for ISO 8601, not javascript ticks
    public DateTime Timestamp { get; set; }
}
