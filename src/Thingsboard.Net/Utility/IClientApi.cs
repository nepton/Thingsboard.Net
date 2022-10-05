namespace Thingsboard.Net.Utility;

public interface IClientApi<out TClientApi> where TClientApi : IClientApi<TClientApi>
{
    TClientApi WithCredentials(TbCredentials credentials);
}
