using DynSec.Model;
using DynSec.Model.Commands;
using DynSec.Model.Responses.TopLevel;
using DynSec.Protocol.Exceptions;
using DynSec.Protocol.Interfaces;

namespace DynSec.Protocol
{
    partial class GroupsService : BaseService, IGroupsService
    {
        public async Task<string?> CreateGroup(Group newgroup)
        {
            if ((newgroup.GroupName == null) || (newgroup.GroupName == ""))
            {
                throw new DynSecProtocolInvalidParameterException("Group name is required");
            }
            var builder = new CreateGroupBuilder(newgroup.GroupName)
                .WithTextDescription(newgroup.TextDescription ?? "")
                .WithTextName(newgroup.TextName ?? "");

            foreach (var role in newgroup.Roles ?? [])
            {
                builder.AddRole(role.RoleName, role.Priority);
            }
            foreach (var client in newgroup.Clients ?? [])
            {
                if (!string.IsNullOrEmpty(client.UserName))
                {
                    builder.AddClient(client.UserName);
                }
            }
            var result = await ExecuteCommand<GeneralResponse>(builder.Build());
            return commandDoneString;
        }

        public async Task<string?> ModifyGroup(Group group)
        {
            if (group.GroupName== null)
            {
                throw new DynSecProtocolInvalidParameterException("Group name is required");
            }
            var builder = new ModifyGroupBuilder(group.GroupName);
            if (group.TextDescription != null)
            {
                builder = (ModifyGroupBuilder)builder.WithTextDescription(group.TextDescription);
            }
            if (group.TextName != null)
            {
                builder = (ModifyGroupBuilder)builder.WithTextName(group.TextName);
            }

            if (group.Roles != null)
            {
                if (group.Roles.Length == 0)
                {
                    builder = (ModifyGroupBuilder)builder.AddEmptyRoleList();
                }
                foreach (var role in group.Roles)
                {
                    builder.AddRole(role.RoleName, role.Priority);
                }
            }
            if (group.Clients != null)
            {
                if (group.Clients.Length == 0)
                {
                    builder = (ModifyGroupBuilder)builder.AddEmptyClientList();
                }
                foreach (var client in group.Clients)
                {
                    if (!string.IsNullOrEmpty(client.UserName))
                    {
                        builder.AddClient(client.UserName);
                    }
                }
            }

            var result = await ExecuteCommand<GeneralResponse>(builder.Build());
            return commandDoneString;
        }

        public async Task<string?> DeleteGroup(string group)
        {
            if (group == null)
            {
                throw new DynSecProtocolInvalidParameterException("Group name is required");
            }
            var cmd = new DeleteGroup(group);
            var result = await ExecuteCommand<GeneralResponse>(cmd);
            return commandDoneString;
        }


    }
}
