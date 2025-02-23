using DynSec.Model;
using DynSec.Model.Responses;
using DynSec.Protocol.Interfaces;

namespace DynSec.GraphQL
{
    public class DynSecMutation
    {
        #region Constructor and fields
        private readonly ICLientsService clientsService;
        private readonly IRolesService rolesService;
        private readonly IGroupsService groupsService;
        private readonly IACLService aclService;
        private readonly DynSecQuery query;

        public DynSecMutation(
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
            query = new DynSecQuery(_clientsService, _rolesService, _groupsService, _aclService);
        }
        #endregion

        #region Clients
        public async Task<string?> CreateClientAsync(Client newclient, string password) => await clientsService.CreateClient(newclient, password);
        public async Task<string?> ModifyClientAsync(Client client, string? password) => await clientsService.ModifyClient(client, password);
        public async Task<string?> DeleteClientAsync(string client) => await clientsService.DeleteClient(client);
        public async Task<string?> EnableClientAsync(string client) => await clientsService.EnableClient(client);
        public async Task<string?> DisableClientAsync(string client) => await clientsService.DisableClient(client);

        #endregion

        #region Roles
        public async Task<string?> CreateRoleAsync(RoleACL newrole) => await rolesService.CreateRole(newrole);
        public async Task<string?> ModifyRoleAsync(RoleACL role) => await rolesService.ModifyRole(role);

        #endregion

        #region ACLs

        public async Task<DefaultACLAccessData?> SetDefaultACLsAsync(List<DefaultACL> data)
        {
            await aclService.SetDefault(data);
            return await query.GetDefaultACLAsync();
        }
        #endregion
    }
}