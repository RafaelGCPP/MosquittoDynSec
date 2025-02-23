using DynSec.Model;
using DynSec.Model.Responses;

namespace DynSec.Protocol.Interfaces
{
    public interface IRolesService
    {
        Task<RoleListData?> GetList(bool? verbose);
        Task<RoleInfoData?> Get(string role);
        Task<string?> CreateRole(RoleACL newrole);
        Task<string?> ModifyRole(RoleACL role);
    }
}