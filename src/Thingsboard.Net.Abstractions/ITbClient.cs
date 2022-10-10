namespace Thingsboard.Net;

public interface ITbClient<out TClient> where TClient : ITbClient<TClient>
{
    TClient WithCredentials(string username, string? password);
}
