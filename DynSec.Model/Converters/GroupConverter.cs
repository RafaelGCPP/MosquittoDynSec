using System.Text.Json;
using System.Text.Json.Serialization;

namespace DynSec.Model.Converters
{
    public class GroupConverter : JsonConverter<Group>
    {
        public override Group? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {

            Group group = new();

            if (reader.TokenType != JsonTokenType.String &&
                reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            if (reader.TokenType == JsonTokenType.String)
            {
                group.GroupName = reader.GetString();
                return group;
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
                    case "groupname":
                        if (!reader.Read() || reader.TokenType != JsonTokenType.String)
                        {
                            throw new JsonException();
                        }
                        group.GroupName = reader.GetString();
                        break;
                    case "textname":
                        if (!reader.Read() || reader.TokenType != JsonTokenType.String)
                        {
                            throw new JsonException();
                        }
                        group.TextName = reader.GetString();
                        break;
                    case "textdescription":
                        if (!reader.Read() || reader.TokenType != JsonTokenType.String)
                        {
                            throw new JsonException();
                        }
                        group.TextDescription = reader.GetString();
                        break;
                    case "roles":
                        if (!reader.Read() || reader.TokenType != JsonTokenType.StartArray)
                        {
                            throw new JsonException();
                        }
                        group.Roles = JsonSerializer.Deserialize<RolePriority[]>(ref reader, options);
                        break;
                    case "clients":
                        if (!reader.Read() || reader.TokenType != JsonTokenType.StartArray)
                        {
                            throw new JsonException();
                        }
                        group.Clients = JsonSerializer.Deserialize<ClientNameClass[]>(ref reader, options);
                        break;
                    default:
                        throw new JsonException($"Invalid property: {propertyName}");

                }

            }


            return group;
        }

        public override void Write(Utf8JsonWriter writer, Group value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            WriteStringProperty(ref writer, "groupName", value.GroupName, options);
            WriteStringProperty(ref writer, "textName", value.TextName, options);
            WriteStringProperty(ref writer, "textDescription", value.TextDescription, options);

            if (value.Roles is not null || options.DefaultIgnoreCondition != JsonIgnoreCondition.WhenWritingNull)
            {
                writer.WritePropertyName("roles");
                JsonSerializer.Serialize(writer, value.Roles, options);
            }

            if (value.Clients is not null || options.DefaultIgnoreCondition != JsonIgnoreCondition.WhenWritingNull)
            {
                writer.WritePropertyName("groups");
                JsonSerializer.Serialize(writer, value.Clients, options);
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
