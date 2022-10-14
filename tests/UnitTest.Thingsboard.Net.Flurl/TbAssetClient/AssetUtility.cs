using Thingsboard.Net;

namespace UnitTest.Thingsboard.Net.Flurl.TbAssetClient;

public class AssetUtility
{
    public static TbAsset GenerateEntity()
    {
        var entity = new TbAsset
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
        var entity = GenerateEntity();
        return await client.SaveAssetAsync(entity);
    }

    public static async Task DeleteAssetAsync(TbEntityId entityId)
    {
        var client = TbTestFactory.Instance.CreateAssetClient();
        await client.DeleteAssetAsync(entityId.Id);
    }
}
