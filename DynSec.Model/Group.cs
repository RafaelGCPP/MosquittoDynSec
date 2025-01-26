using DynSec.Model.Converters;
using System.Text.Json.Serialization;

namespace DynSec.Model
{
    [JsonConverter(typeof(GroupConverter))]
    public class Group
    {
        public string? GroupName { get; set; }
        public string? TextName { get; set; }
        public string? TextDescription { get; set; }
        public RoleNameClass[]? Roles { get; set; }
        public ClientNameClass[]? Clients { get; set; }
    }
}
