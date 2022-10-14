using Thingsboard.Net;

namespace UnitTest.Thingsboard.Net.Flurl.TbCustomerClient;

public class CustomerUtility
{
    public static TbNewCustomer GenerateNewCustomer()
    {
        var entity = new TbNewCustomer()
        {
            Name     = Guid.NewGuid().ToString(),
            Title    = Guid.NewGuid().ToString(),
            Address  = Guid.NewGuid().ToString(),
            Address2 = Guid.NewGuid().ToString(),
            City     = Guid.NewGuid().ToString(),
            Country  = Guid.NewGuid().ToString(),
            Email    = $"nepton_{new Random().Next()}@abc.com",
            Phone    = Guid.NewGuid().ToString(),
            State    = Guid.NewGuid().ToString(),
            Zip      = Guid.NewGuid().ToString()
        };

        return entity;
    }

    public static async Task<TbCustomer> CreateEntityAsync()
    {
        var client = TbTestFactory.Instance.CreateCustomerClient();
        var entity = GenerateNewCustomer();
        return await client.SaveCustomerAsync(entity);
    }
}
