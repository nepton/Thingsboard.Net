using System.Net;
using Thingsboard.Net;
using Thingsboard.Net.Exceptions;

namespace UnitTest.Thingsboard.Net.Flurl.TbCommon;

public class TbCommonTestHelper
{
    /// <summary>
    /// Test client with incorrect base url
    /// </summary>
    /// <param name="client"></param>
    /// <param name="act"></param>
    /// <typeparam name="TClient"></typeparam>
    public async Task TestIncorrectBaseUrl<TClient>(TClient client, Func<TClient, Task> act) where TClient : ITbClient<TClient>
    {
        // arrange
        var ex = await Assert.ThrowsAsync<TbHttpException>(async () =>
        {
            await act(client.WithBaseUrl("http://localhost:1234") // incorrect url
                .WithHttpTimeout(1, 0, 0));
        });

        Assert.False(ex.Completed);
    }

    /// <summary>
    /// Test client with correct username
    /// </summary>
    /// <param name="client"></param>
    /// <param name="act"></param>
    /// <typeparam name="TClient"></typeparam>
    public async Task TestIncorrectUsername<TClient>(TClient client, Func<TClient, Task> act) where TClient : ITbClient<TClient>
    {
        // arrange
        var ex = await Assert.ThrowsAsync<TbHttpException>(async () =>
        {
            await act(client.WithCredentials(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));
        });

        Assert.Equal(HttpStatusCode.Unauthorized, ex.StatusCode);
    }
}
