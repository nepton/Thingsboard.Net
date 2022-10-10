using System;

namespace Thingsboard.Net;

/// <summary>
/// Allows to query for Api Usage based on optional customer id.
/// For example, this entity filter selects the 'Api Usage' entity for customer with id 'e6501f30-2a7a-11ec-94eb-213c95f54092':
/// </summary>
public class TbApiUsageFilter : TbEntityFilter
{
    /// <summary>
    /// The type of filter
    /// </summary>
    public override string Type => "apiUsageState";

    /// <summary>
    /// If the customer id is not set, returns current tenant API usage.
    /// </summary>
    public TbEntityId? CustomerId { get; set; }

    public TbApiUsageFilter()
    {
    }

    public TbApiUsageFilter(Guid customerId)
    {
        CustomerId = new TbEntityId(TbEntityType.CUSTOMER, customerId);
    }
}
