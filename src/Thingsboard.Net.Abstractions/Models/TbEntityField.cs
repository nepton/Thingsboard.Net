namespace Thingsboard.Net.Models;

/// <summary>
/// The properties of the ENTITY object contain all types of FIELD properties, including native fields, properties, and telemetry fields
/// </summary>
public class TbEntityField
{
    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public TbEntityField(string key, TbEntityFieldType type)
    {
        Key  = key;
        Type = type;
    }

    public string            Key  { get; }
    public TbEntityFieldType Type { get; }

    public object ToQuery()
    {
        return new
        {
            type = Type,
            key  = Key,
        };
    }
}
