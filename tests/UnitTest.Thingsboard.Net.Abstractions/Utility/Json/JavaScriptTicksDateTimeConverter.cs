using Newtonsoft.Json;

namespace UnitTest.Thingsboard.Net.Abstractions.Utility.Json;

/// <summary>
/// Convert a long value from 1970 to a DateTime value
/// NOTE, Must handling DateTime?
/// </summary>
public class JavaScriptTicksDateTimeConverter : JsonConverter<DateTime?>
{
    /// <summary>Writes the JSON representation of the object.</summary>
    /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
    /// <param name="value">The value.</param>
    /// <param name="serializer">The calling serializer.</param>
    public override void WriteJson(JsonWriter writer, DateTime? value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToJavaScriptTicks());
    }

    /// <summary>Reads the JSON representation of the object.</summary>
    /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
    /// <param name="objectType">Type of the object.</param>
    /// <param name="existingValue">The existing value of object being read. If there is no existing value then <c>null</c> will be used.</param>
    /// <param name="hasExistingValue">The existing value has a value.</param>
    /// <param name="serializer">The calling serializer.</param>
    /// <returns>The object value.</returns>
    public override DateTime? ReadJson(JsonReader reader, Type objectType, DateTime? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        try
        {
            if (reader.TokenType == JsonToken.String)
            {
                var str = reader.Value?.ToString();
                if (string.IsNullOrEmpty(str))
                    return default;

                if (!long.TryParse(str, out var longValue))
                    return existingValue;

                return longValue.ToDateTime();
            }

            if (reader.TokenType == JsonToken.Integer)
            {
                var longValue = (long) reader.Value!;
                return longValue.ToDateTime();
            }
        }
        catch (Exception ex)
        {
            throw new JsonSerializationException($"Error converting value {reader.Value} to type '{objectType}'.", ex);
        }

        throw new JsonSerializationException("Unexpected token parsing date. Expected String or Integer, got " + reader.TokenType);
    }
}
