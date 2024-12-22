using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Database.Services;

public class AttributeConverter : JsonConverter<IDictionary<string, object>>
{
    public override IDictionary<string, object> Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        IDictionary<string, object> output = new Dictionary<string, object>();

        while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
        {
            var propertyName = reader.GetString();
            reader.Read();
            var propertyValue = FromJsonTokenType(reader);
            output.Add(propertyName, propertyValue);
        }

        return output;
    }

    private static object FromJsonTokenType(Utf8JsonReader reader)
    {
        return reader.TokenType switch
        {
            JsonTokenType.Number => reader.GetDecimal(),
            JsonTokenType.String => reader.TryGetDateTime(out var value)
                ? (object)value
                : reader.GetString(),
            JsonTokenType.True => reader.GetBoolean(),
            JsonTokenType.False => reader.GetBoolean(),
            _ => null
        };
    }

    public override void Write(Utf8JsonWriter writer, IDictionary<string, object> value,
        JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (var (propertyName, propertyValue) in value)
        {
            switch (propertyValue)
            {
                case decimal x:
                    writer.WriteNumber(propertyName, x);
                    break;
                case bool x:
                    writer.WriteBoolean(propertyName, x);
                    break;
                case string _:
                case DateTime _:
                    writer.WriteString(propertyName, propertyValue.ToString());
                    break;
            }
        }

        writer.WriteEndObject();
    }
}