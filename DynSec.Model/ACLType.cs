using System.Text.Json.Serialization;

namespace DynSec.Model
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ACLType
    {
        publishClientSend,
        publishClientReceive,
        subscribeLiteral,
        subscribePattern,
        unsubscribeLiteral,
        unsubscribePattern
    }

}
