using Thingsboard.Net;

namespace UnitTest.Thingsboard.Net.Flurl.TbAssetClient;

public class AssetUtility
{
    public static TbNewAsset GenerateNewAsset()
    {
        var entity = new TbNewAsset
        {
            Name  = Guid.NewGuid().ToString(),
            Type  = Guid.NewGuid().ToString(),
            Label = Guid.NewGuid().ToString(),
        };

        return entity;
    }

    public static async Task<TbAsset> CreateAssetAsync()
    {
        var client = TbTestFactory.Instance.CreateAssetClient();
        var entity = GenerateNewAsset();
        return await client.SaveAssetAsync(entity);
    }

    public static async Task DeleteAssetAsync(TbEntityId entityId)
    {
        var client = TbTestFactory.Instance.CreateAssetClient();
        await client.DeleteAssetAsync(entityId.Id);
    }
}
