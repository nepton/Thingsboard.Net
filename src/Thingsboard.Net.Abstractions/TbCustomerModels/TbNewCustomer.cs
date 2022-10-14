using System;
using System.Collections.Generic;

namespace Thingsboard.Net;

public class TbNewCustomer
{
    public string?                      Title          { get; set; }
    public string?                      Name           { get; set; }
    public TbEntityId?                  TenantId       { get; set; }
    public string?                      Country        { get; set; }
    public string?                      State          { get; set; }
    public string?                      City           { get; set; }
    public string?                      Address        { get; set; }
    public string?                      Address2       { get; set; }
    public string?                      Zip            { get; set; }
    public string?                      Phone          { get; set; }
    public string?                      Email          { get; set; }
    public Dictionary<string, object?>? AdditionalInfo { get; set; }
}
