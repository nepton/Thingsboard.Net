using System;
using System.Collections.Generic;

namespace Thingsboard.Net;

public class TbQueue
{
    public TbQueue(TbEntityId id)
    {
        Id = id;
    }

    public TbEntityId Id { get; }

    public DateTime                    CreatedTime           { get; set; }
    public Dictionary<string, object>? AdditionalInfo        { get; set; }
    public TbEntityId?                 TenantId              { get; set; }
    public string?                     Name                  { get; set; }
    public string?                     Topic                 { get; set; }
    public int                         PollInterval          { get; set; }
    public int                         Partitions            { get; set; }
    public bool                        ConsumerPerPartition  { get; set; }
    public int                         PackProcessingTimeout { get; set; }
    public TbQueueSubmitStrategy?      SubmitStrategy        { get; set; }
    public TbQueueProcessingStrategy?  ProcessingStrategy    { get; set; }
}
