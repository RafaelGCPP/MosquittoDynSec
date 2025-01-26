using DynSec.Model.Converters;
using System.Text.Json.Serialization;

namespace DynSec.Model.Responses.Abstract
{
    [JsonConverter(typeof(ResponseConverter))]
    public abstract class AbstractResponse

    {
        public string? Command { get; set; }
        public string? Error { get; set; } = "Ok";

    }
}
