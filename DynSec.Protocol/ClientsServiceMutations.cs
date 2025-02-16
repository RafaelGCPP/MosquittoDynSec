using DynSec.Model;
using DynSec.Model.Commands;
using DynSec.Model.Responses.TopLevel;
using DynSec.Protocol.Exceptions;
using DynSec.Protocol.Interfaces;
using System.Net;

namespace DynSec.Protocol
{
    public partial class ClientsService : ICLientsService
    {
        public async Task<string?> CreateClient(Client newclient, string password)
        {
            if (newclient.UserName == null)
            {
                throw new DynSecProtocolInvalidParameterException("Client username is required");
            }

            var builder = new CreateClientBuilder(newclient.UserName, password)          
                .WithTextDescription(newclient.TextDescription ?? "")
                .WithTextName(newclient.TextName ?? "");

            foreach (var role in newclient.Roles ?? [])
            {
                builder.AddRole(role.RoleName ?? "", role.Priority);
            }
            foreach (var group in newclient.Groups ?? [])
            {
                builder.AddRole(group.GroupName ?? "", group.Priority);
            }

            var result = await ExecuteCommand<GeneralResponse>(builder.Build());
            return commandDoneString;
        }

        public async Task<string?> ModifyClient(Client client, string? password)
        {
            if (client.UserName == null)
            {
                throw new DynSecProtocolInvalidParameterException("Client username is required");
            }

            var builder = new ModifyClientBuilder(client.UserName).WithPassword(password);

            if (client.TextDescription != null)
            {
                builder = (ModifyClientBuilder)builder.WithTextDescription(client.TextDescription);
            }
            if (client.TextName != null)
            {
                builder = (ModifyClientBuilder)builder.WithTextName(client.TextName);
            }

            if (client.Roles != null)
            {
                if (client.Roles.Length == 0)
                {
                    builder = (ModifyClientBuilder)builder.AddEmptyRoleSet();
                }

                foreach (var role in client.Roles)
                {
                    builder = (ModifyClientBuilder)builder.AddRole(role.RoleName, role.Priority);
                }
            }

            if (client.Groups != null)
            {
                if (client.Groups.Length == 0)
                {
                    builder = (ModifyClientBuilder)builder.AddEmptyGroupSet();
                }
                foreach (var group in client.Groups)
                {
                    builder = (ModifyClientBuilder)builder.AddGroup(group.GroupName, group.Priority);
                }
            }


            var command = builder.Build();
            var result = await ExecuteCommand<GeneralResponse>(command);
            return commandDoneString;
        }

        public async Task<string?> DeleteClient(string client)
        {
            var cmd = new DeleteClient(client);
            var result = await ExecuteCommand<GeneralResponse>(cmd);
            return commandDoneString;
        }

        public async Task<string?> EnableClient(string client)
        {
            var cmd = new EnableClient(client);
            var result = await ExecuteCommand<GeneralResponse>(cmd);
            return commandDoneString;
        }

        public async Task<string?> DisableClient(string client)
        {
            var cmd = new DisableClient(client);
            var result = await ExecuteCommand<GeneralResponse>(cmd);
            return commandDoneString;
        }
    }
}