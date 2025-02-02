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
        public async Task<ClientInfoData?> CreateClientAsync(Client newclient, string password)
        {
            await clientsService.CreateClient(newclient, password);
            return await query.GetClientAsync(newclient.UserName ?? "");
        }
        public async Task<ClientInfoData?> ModifyClientAsync(Client client, string? password)
        {
            await clientsService.ModifyClient(client, password);
            return await query.GetClientAsync(client.UserName ?? "");
        }
        public async Task<string?> DeleteClientAsync(string client) => await clientsService.DeleteClient(client);

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