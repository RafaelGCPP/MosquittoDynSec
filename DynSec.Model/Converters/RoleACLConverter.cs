using System.Text.Json;
using System.Text.Json.Serialization;

namespace DynSec.Model.Converters
{
    public class RoleACLConverter : JsonConverter<RoleACL>
    {
        public override RoleACL? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            RoleACL roleACL = new();

            if (reader.TokenType != JsonTokenType.String &&
                reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            if (reader.TokenType == JsonTokenType.String)
            {
                roleACL.RoleName = reader.GetString();
                return roleACL;
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
                    case "rolename":
                        if (!reader.Read() || reader.TokenType != JsonTokenType.String)
                        {
                            throw new JsonException();
                        }
                        roleACL.RoleName = reader.GetString();
                        break;
                    case "textname":
                        if (!reader.Read() || reader.TokenType != JsonTokenType.String)
                        {
                            throw new JsonException();
                        }
                        roleACL.TextName = reader.GetString();
                        break;
                    case "textdescription":
                        if (!reader.Read() || reader.TokenType != JsonTokenType.String)
                        {
                            throw new JsonException();
                        }
                        roleACL.TextDescription = reader.GetString();
                        break;
                    case "acls":
                        if (!reader.Read() || reader.TokenType != JsonTokenType.StartArray)
                        {
                            throw new JsonException();
                        }
                        roleACL.ACLs = JsonSerializer.Deserialize<ACLDefinition[]>(ref reader, options);
                        break;

                    default:
                        throw new JsonException($"Invalid property: {propertyName}");

                }

            }

            return roleACL;
        }

        public override void Write(Utf8JsonWriter writer, RoleACL value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            WriteStringProperty(ref writer, "roleName", value.RoleName, options);
            WriteStringProperty(ref writer, "textName", value.TextName, options);
            WriteStringProperty(ref writer, "textDescription", value.TextDescription, options);



            if (value.ACLs is not null || options.DefaultIgnoreCondition != JsonIgnoreCondition.WhenWritingNull)
            {
                writer.WritePropertyName("acls");
                JsonSerializer.Serialize(writer, value.ACLs, options);
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
