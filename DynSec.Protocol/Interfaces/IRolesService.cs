using DynSec.Model.Responses;

namespace DynSec.Protocol.Interfaces
{
    public interface IRolesService
    {
        Task<RoleListData?> GetList(bool? verbose);
        Task<RoleInfoData?> Get(string role);
    }
}