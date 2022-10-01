using System;

namespace Thingsboard.Net.Models;

public class TbResponseError
{
    public int      Status    { get; set; }
    public string?  Message   { get; set; }
    public int      ErrorCode { get; set; }
    public DateTime Timestamp { get; set; }
}
