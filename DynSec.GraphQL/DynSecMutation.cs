using DynSec.Protocol.Interfaces;

namespace DynSec.GraphQL
{
    public class DynSecMutation
    {
        private readonly ICLientsService clientsService;
        private readonly IRolesService rolesService;
        private readonly IGroupsService groupsService;
        private readonly IACLService aclService;

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
        }
    }
}