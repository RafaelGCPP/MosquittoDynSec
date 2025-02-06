using DynSec.Model.Converters;
using System.Text.Json.Serialization;

namespace DynSec.Model
{

    [JsonConverter(typeof(ClientConverter))]
    public class Client
    {
        public string? UserName { get; set; }
        public string? TextName { get; set; }
        public string? TextDescription { get; set; }
        public bool? Disabled { get; set; }
        public RolePriority[]? Roles { get; set; }
        public GroupPriority[]? Groups { get; set; }
    }

}
