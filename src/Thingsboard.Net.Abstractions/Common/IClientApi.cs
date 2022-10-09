namespace Thingsboard.Net.Common;

public interface IClientApi<out TClientApi> where TClientApi : IClientApi<TClientApi>
{
    TClientApi WithCredentials(string username, string? password);
}
