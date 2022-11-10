using System.Collections.Concurrent;
using Thingsboard.Net;

namespace UnitTest.Thingsboard.Net.Flurl;

public class TbTestData
{
    public static Guid GetTestDeviceId() => GetDeviceByName(TestDeviceName).Id.Id;

    public static readonly string TestDeviceName = "Test Device A1";

    public static Guid GetTestDeviceId2() => GetDeviceByName(TestDeviceName2).Id.Id;

    public static string TestDeviceName2 = "Test Device A2";

    public static Guid GetTestCustomerId() => GetCustomerByName(TestCustomerTitle).Id.Id;

    public static string TestCustomerTitle = "Customer A";

    public static Guid GetTestDeviceProfileId() => GetDeviceByName(TestDeviceName).DeviceProfileId?.Id ?? throw new Exception("Device profile id is null");

    private static readonly ConcurrentDictionary<string, TbDevice> _deviceCache = new();

    private static readonly object _locker = new();

    private static TbDevice GetDeviceByName(string name)
    {
        lock (_locker)
        {
            return _deviceCache.GetOrAdd(name,
                _ =>
                {
                    var tbDeviceClient = TbTestFactory.Instance.CreateDeviceClient();
                    var device         = tbDeviceClient.GetTenantDeviceByNameAsync(name).Result;
                    return device ?? throw new Exception("Device not found");
                });
        }
    }

    private static readonly ConcurrentDictionary<string, TbCustomer> _customerCache = new();

    private static TbCustomer GetCustomerByName(string name)
    {
        lock (_locker)
        {
            return _customerCache.GetOrAdd(name,
                _ =>
                {
                    var client   = TbTestFactory.Instance.CreateCustomerClient();
                    var customer = client.GetTenantCustomerAsync(name).Result;
                    return customer ?? throw new Exception("Customer not found");
                });
        }
    }
}
