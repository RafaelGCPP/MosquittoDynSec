using DynSec.Model.Commands;
using DynSec.Model.Responses;
using DynSec.Protocol.Interfaces;

namespace DynSec.Protocol
{
    public partial class RolesService(IDynamicSecurityHandler _handler) : BaseService(_handler), IRolesService
    {
        public async Task<RoleListData?> GetList(bool? verbose)
        {
            var cmd = new ListRoles(verbose ?? true);
            var result = await ExecuteCommand<RoleList>(cmd);
            return result.Data;
        }

        public async Task<RoleInfoData?> Get(string role)
        {
            var cmd = new GetRole(role);
            var result = await ExecuteCommand<RoleInfo>(cmd);
            return result.Data;
        }

    }
}
