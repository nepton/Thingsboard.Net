namespace Thingsboard.Net;

/// <summary>
/// The properties of the ENTITY object contain all types of FIELD properties, including native fields, properties, and telemetry fields
/// </summary>
public record TbEntityField(string Key, TbEntityFieldType Type)
{
    /// <summary>
    /// The key of the field
    /// </summary>
    public string Key { get; } = Key;

    /// <summary>
    /// The type of the field
    /// </summary>
    public TbEntityFieldType Type { get; } = Type;
}
