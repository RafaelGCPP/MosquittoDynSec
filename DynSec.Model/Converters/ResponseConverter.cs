using DynSec.Model.Responses;
using DynSec.Model.Responses.Abstract;
using DynSec.Model.Responses.TopLevel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DynSec.Model.Converters
{
    public class ResponseConverter : JsonConverter<AbstractResponse>
    {
        public override bool CanConvert(Type typeToConvert) =>
            typeof(AbstractResponse).IsAssignableFrom(typeToConvert);

        public override AbstractResponse Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {

            Utf8JsonReader readerClone = reader;

            if (readerClone.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            readerClone.Read();
            if (readerClone.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            string? propertyName = readerClone.GetString();
            if (propertyName != "command")
            {
                throw new JsonException();
            }

            readerClone.Read();
            if (readerClone.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }

            string? command = readerClone.GetString();

            AbstractResponse response = command switch
            {
                "listClients" => JsonSerializer.Deserialize<ClientList>(ref reader, options)!,
                "listGroups" => JsonSerializer.Deserialize<GroupList>(ref reader, options)!,
                "listRoles" => JsonSerializer.Deserialize<RoleList>(ref reader, options)!,
                "getDefaultACLAccess" => JsonSerializer.Deserialize<DefaultACLAccess>(ref reader, options)!,
                "getClient" => JsonSerializer.Deserialize<ClientInfo>(ref reader, options)!,
                "getGroup" => JsonSerializer.Deserialize<GroupInfo>(ref reader, options)!,
                "getAnonymousGroup" => JsonSerializer.Deserialize<AnonymousGroupInfo>(ref reader, options)!,
                "getRole" => JsonSerializer.Deserialize<RoleInfo>(ref reader, options)!,
                _ => JsonSerializer.Deserialize<GeneralResponse>(ref reader, options)!,
            };

            return response;
        }






        public override void Write(Utf8JsonWriter writer, AbstractResponse response, JsonSerializerOptions options)
        {
            var options2 = new JsonSerializerOptions(options)
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };


            switch (response)
            {
                case ClientList l:
                    JsonSerializer.Serialize(writer, l, options2);
                    break;
                case GroupList l:
                    JsonSerializer.Serialize(writer, l, options2);
                    break;
                case RoleList l:
                    JsonSerializer.Serialize(writer, l, options2);
                    break;
                case DefaultACLAccess d:
                    JsonSerializer.Serialize(writer, d, options2);
                    break;
                case ClientInfo c:
                    JsonSerializer.Serialize(writer, c, options2);
                    break;
                case GroupInfo g:
                    JsonSerializer.Serialize(writer, g, options2);
                    break;
                case GeneralResponse g:
                    JsonSerializer.Serialize(writer, g, options2);
                    break;
                default:
                    writer.WriteStartObject();
                    writer.WriteEndObject();
                    break;
            }

        }
    }
}
