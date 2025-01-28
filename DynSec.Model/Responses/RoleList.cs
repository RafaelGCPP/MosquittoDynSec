using DynSec.Model.Responses.Abstract;

namespace DynSec.Model.Responses
{
    public sealed class RoleList : AbstractResponse
    {

        public  RoleListData? Data { get; set; }
    }

    public sealed class RoleListData
    {
        public int? TotalCount { get; set; }
        public RoleACL[]? Roles { get; set; }

    };
}
