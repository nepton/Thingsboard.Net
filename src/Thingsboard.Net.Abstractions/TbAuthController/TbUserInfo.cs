using Thingsboard.Net.Models;

namespace Thingsboard.Net.TbAuthController;

public class TbUserInfo
{
    public TbUserInfo(
        TbEntityId           id,
        long                 createdTime,
        TbUserAdditionalInfo additionalInfo,
        TbEntityId           tenantId,
        TbEntityId           customerId,
        string               email,
        string               authority,
        object               firstName,
        object               lastName,
        string               name
    )
    {
        this.Id             = id;
        this.CreatedTime    = createdTime;
        this.AdditionalInfo = additionalInfo;
        this.TenantId       = tenantId;
        this.CustomerId     = customerId;
        this.Email          = email;
        this.Authority      = authority;
        this.FirstName      = firstName;
        this.LastName       = lastName;
        this.Name           = name;
    }

    public TbEntityId           Id             { get; }
    public long                 CreatedTime    { get; }
    public TbUserAdditionalInfo AdditionalInfo { get; }
    public TbEntityId           TenantId       { get; }
    public TbEntityId           CustomerId     { get; }
    public string               Email          { get; }
    public string               Authority      { get; }
    public object               FirstName      { get; }
    public object               LastName       { get; }
    public string               Name           { get; }
}
