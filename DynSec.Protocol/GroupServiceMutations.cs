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
                if (client.UserName == "")
                {
                    builder.AddClient(client.UserName);
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
