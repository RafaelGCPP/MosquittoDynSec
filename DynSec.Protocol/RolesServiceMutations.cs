using DynSec.Model;
using DynSec.Model.Commands;
using DynSec.Model.Responses.TopLevel;
using DynSec.Protocol.Exceptions;
using DynSec.Protocol.Interfaces;

namespace DynSec.Protocol
{
    public partial class RolesService : BaseService, IRolesService
    {
          
        public async Task<string?> CreateRole(RoleACL newrole)
        {
            if (newrole.RoleName == null)
            {
                throw new DynSecProtocolInvalidParameterException("Role name is required");
            }
            var builder = new CreateRoleBuilder(newrole.RoleName)
                .WithTextDescription(newrole.TextDescription ?? "")
                .WithTextName(newrole.TextName ?? "");

            foreach (var permission in newrole.ACLs ?? [])
            {
                builder.AddACLRule(
                    permission.Topic, 
                    permission.ACLType, 
                    permission.Priority, 
                    permission.Allow);
            }
            var result = await ExecuteCommand<GeneralResponse>(builder.Build());
            return commandDoneString;
        }

        public async Task<string?> ModifyRole(RoleACL role)
        {
            if (role.RoleName == null)
            {
                throw new DynSecProtocolInvalidParameterException("Role name is required");
            }
            var builder = new ModifyRoleBuilder(role.RoleName);
            if (role.TextDescription != null)
            {
                builder = (ModifyRoleBuilder)builder.WithTextDescription(role.TextDescription);
            }
            if (role.TextName != null)
            {
                builder = (ModifyRoleBuilder)builder.WithTextName(role.TextName);
            }
            if (role.ACLs != null)
            {
                if (role.ACLs.Length == 0)
                {
                    builder = (ModifyRoleBuilder)builder.AddEmptyACLSet();
                }
                foreach (var permission in role.ACLs)
                {
                    builder.AddACLRule(
                        permission.Topic,
                        permission.ACLType,
                        permission.Priority,
                        permission.Allow);
                }
            }
            var result = await ExecuteCommand<GeneralResponse>(builder.Build());
            return commandDoneString;
        }
    }
}
