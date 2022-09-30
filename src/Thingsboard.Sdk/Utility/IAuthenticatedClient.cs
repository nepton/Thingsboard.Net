namespace Thingsboard.Sdk.Utility;

public interface IAuthenticatedClient<out TDerived> where TDerived : IAuthenticatedClient<TDerived>
{
    TDerived WithAuthenticate(string username, string password);
}
