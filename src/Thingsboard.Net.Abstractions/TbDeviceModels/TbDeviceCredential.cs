using System;

namespace Thingsboard.Net;

public class TbDeviceCredential
{
    public TbEntityId? Id               { get; set; }
    public DateTime    CreatedTime      { get; set; }
    public TbEntityId? DeviceId         { get; set; }
    public string?     CredentialsType  { get; set; }
    public string?     CredentialsId    { get; set; }
    public string?     CredentialsValue { get; set; }
}
