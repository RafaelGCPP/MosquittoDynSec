using DynSec.Model.Responses;
using DynSec.Protocol.Interfaces;

namespace DynSec.GraphQL
{
    public class DynSecQuery
    {
        #region Constructor and fields
        private readonly ICLientsService clientsService;
        private readonly IRolesService rolesService;
        private readonly IGroupsService groupsService;
        private readonly IACLService aclService;

        public DynSecQuery(
            ICLientsService _clientsService,
            IRolesService _rolesService,
            IGroupsService _groupsService,
            IACLService _aclService
            )
        {
            clientsService = _clientsService;
            rolesService = _rolesService;
            groupsService = _groupsService;
            aclService = _aclService;
        }
        #endregion

        #region Clients

        public async Task<ClientListData?> GetClientsListAsync(bool? verbose) => await clientsService.GetList(verbose);
        public async Task<ClientInfoData?> GetClientAsync(string client) => await clientsService.Get(client);

        #endregion

        #region Roles

        public async Task<RoleListData?> GetRolesListAsync(bool? verbose) => await rolesService.GetList(verbose);
        public async Task<RoleInfoData?> GetRoleAsync(string role) => await rolesService.Get(role);
        #endregion

        #region Groups

        public async Task<GroupListData?> GetGroupsListAsync(bool? verbose) => await groupsService.GetList(verbose);
        public async Task<GroupInfoData?> GetGroupAsync(string group) => await groupsService.Get(group);
        public async Task<AnonymousGroupInfoData?> GetAnonymousGroupAsync() => await groupsService.GetAnonymous();
        #endregion

        #region ACLs
        public async Task<DefaultACLAccessData?> GetDefaultACLAsync() => await aclService.GetDefault();

        #endregion
    }
}
