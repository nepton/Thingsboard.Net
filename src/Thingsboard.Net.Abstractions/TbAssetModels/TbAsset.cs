using System;
using System.Collections.Generic;

namespace Thingsboard.Net;

public class TbAsset
{
    public TbEntityId?                  Id             { get; set; }
    public DateTime                     CreatedTime    { get; set; }
    public TbEntityId?                  TenantId       { get; set; }
    public TbEntityId?                  CustomerId     { get; set; }
    public string?                      Name           { get; set; }
    public string?                      Type           { get; set; }
    public string?                      Label          { get; set; }
    public Dictionary<string, object?>? AdditionalInfo { get; set; }
}
