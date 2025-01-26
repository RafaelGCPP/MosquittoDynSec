using System.Text.Json.Serialization;

namespace DynSec.Model.Responses.Abstract
{
    [JsonDerivedType(typeof(AnonymousGroupInfoData))]
    [JsonDerivedType(typeof(ClientInfoData))]
    [JsonDerivedType(typeof(ClientListData))]
    [JsonDerivedType(typeof(GroupInfoData))]
    [JsonDerivedType(typeof(GroupListData))]
    [JsonDerivedType(typeof(RoleListData))]
    public abstract class AbstractResponseData
    {
    }
}
