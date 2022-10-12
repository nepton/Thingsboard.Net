namespace Thingsboard.Net;

public class TbQueueProcessingStrategy
{
    public int     FailurePercentage      { get; set; }
    public int     MaxPauseBetweenRetries { get; set; }
    public int     PauseBetweenRetries    { get; set; }
    public int     Retries                { get; set; }
    public string? Type                   { get; set; }
}
