using Thingsboard.Net;

namespace UnitTest.Thingsboard.Net.Flurl;

public class TbTestFixture : IDisposable
{
    public TbTestFixture()
    {
        var tbDeviceClient = TbTestFactory.Instance.CreateDeviceClient();
        Device  = tbDeviceClient.GetTenantDeviceByNameAsync("Test Device A1").Result ?? throw new Exception("Test Device A1 not found");
        Device2 = tbDeviceClient.GetTenantDeviceByNameAsync("Test Device A2").Result ?? throw new Exception("Test Device A2 not found");
        
        var tbCustomerClient = TbTestFactory.Instance.CreateCustomerClient();
        Customer = tbCustomerClient.GetTenantCustomerAsync("Customer A").Result ?? throw new Exception("Customer A not found");
        
        var deviceProfileClient = TbTestFactory.Instance.CreateDeviceProfileClient();
        DeviceProfile = deviceProfileClient.GetDefaultDeviceProfileInfoAsync().Result ?? throw new Exception("Default device profile not found");
    }

    /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
    public void Dispose()
    {
    }

    public TbDevice Device { get; }

    public Guid DeviceId => Device.Id.Id;

    public TbDevice Device2 { get; }

    public TbCustomer Customer { get; }

    public Guid CustomerId => Customer.Id.Id;

    public TbDeviceProfileInfo DeviceProfile { get; }

    public Guid DeviceProfileId => DeviceProfile.Id.Id;
}
