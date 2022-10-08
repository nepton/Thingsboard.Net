namespace Thingsboard.Net.Models;

public static class DeviceFields
{
    public static TbEntityField Name  => new("name", TbEntityFieldType.ENTITY_FIELD);
    public static TbEntityField Label => new("label", TbEntityFieldType.ENTITY_FIELD);
}
