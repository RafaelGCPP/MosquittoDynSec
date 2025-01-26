using System.Text.Json;
using System.Text.Json.Serialization;

namespace DynSec.Model.Converters
{
    public class ClientConverter : JsonConverter<Client>
    {
        public override Client? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {

            Client client = new();

            if (reader.TokenType != JsonTokenType.String &&
                reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            if (reader.TokenType == JsonTokenType.String)
            {
                client.UserName = reader.GetString();
                return client;
            }

            while (reader.TokenType != JsonTokenType.EndObject)
            {
                if (!reader.Read() ||
                    reader.TokenType != JsonTokenType.PropertyName &&
                     reader.TokenType != JsonTokenType.EndObject)
                {
                    throw new JsonException();
                }

                if (reader.TokenType == JsonTokenType.EndObject) break;

                string? propertyName = reader.GetString()?.ToLower();

                switch (propertyName)
                {
                    case "username":
                        if (!reader.Read() || reader.TokenType != JsonTokenType.String)
                        {
                            throw new JsonException();
                        }
                        client.UserName = reader.GetString();
                        break;
                    case "textname":
                        if (!reader.Read() || reader.TokenType != JsonTokenType.String)
                        {
                            throw new JsonException();
                        }
                        client.TextName = reader.GetString();
                        break;
                    case "textdescription":
                        if (!reader.Read() || reader.TokenType != JsonTokenType.String)
                        {
                            throw new JsonException();
                        }
                        client.TextDescription = reader.GetString();
                        break;
                    case "roles":
                        if (!reader.Read() || reader.TokenType != JsonTokenType.StartArray)
                        {
                            throw new JsonException();
                        }
                        client.Roles = JsonSerializer.Deserialize<RoleNameClass[]>(ref reader, options);
                        break;
                    case "groups":
                        if (!reader.Read() || reader.TokenType != JsonTokenType.StartArray)
                        {
                            throw new JsonException();
                        }
                        client.Groups = JsonSerializer.Deserialize<GroupNameClass[]>(ref reader, options);
                        break;
                    default:
                        throw new JsonException($"Invalid property: {propertyName}");

                }

            }


            return client;
        }

        public override void Write(Utf8JsonWriter writer, Client value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            WriteStringProperty(ref writer, "userName", value.UserName, options);
            WriteStringProperty(ref writer, "textName", value.TextName, options);
            WriteStringProperty(ref writer, "textDescription", value.TextDescription, options);

            if (value.Roles is not null || options.DefaultIgnoreCondition != JsonIgnoreCondition.WhenWritingNull)
            {
                writer.WritePropertyName("roles");
                JsonSerializer.Serialize(writer, value.Roles, options);
            }

            if (value.Groups is not null || options.DefaultIgnoreCondition != JsonIgnoreCondition.WhenWritingNull)
            {
                writer.WritePropertyName("groups");
                JsonSerializer.Serialize(writer, value.Groups, options);
            }

            writer.WriteEndObject();
        }

        private static void WriteStringProperty(ref Utf8JsonWriter writer, string propertyName, string? value, JsonSerializerOptions options)
        {
            if (options.DefaultIgnoreCondition == JsonIgnoreCondition.WhenWritingNull && value is null) return;

            writer.WritePropertyName(propertyName);
            writer.WriteStringValue(value);
        }

    }
}
