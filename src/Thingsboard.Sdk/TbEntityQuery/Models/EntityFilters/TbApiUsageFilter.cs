using System;

namespace Thingsboard.Sdk.TbEntityQuery;

/// <summary>
/// Allows to query for Api Usage based on optional customer id.
/// For example, this entity filter selects the 'Api Usage' entity for customer with id 'e6501f30-2a7a-11ec-94eb-213c95f54092':
/// </summary>
public class TbApiUsageFilter : TbEntityFilter
{
    /// <summary>
    /// If the customer id is not set, returns current tenant API usage.
    /// </summary>
    public Guid? CustomerId { get; set; }

    public override object ToQuery()
    {
        var query = new
        {
            type = "apiUsageState",
            customerId = CustomerId is { } customerId
                ? new
                {
                    id         = customerId,
                    entityType = "CUSTOMER"
                }
                : null
        };

        return query;
    }
}
