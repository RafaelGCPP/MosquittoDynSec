using DynSec.Model;
using DynSec.Model.Commands;
using DynSec.Model.Responses.TopLevel;
using DynSec.Protocol.Interfaces;
using System.Net;

namespace DynSec.Protocol
{
    public partial class ClientsService : ICLientsService
    {
        public async Task<string?> CreateClient(Client newclient, string password)
        {
            var builder = new CreateClientBuilder(newclient.UserName ?? "", password)
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
            var builder = new ModifyClientBuilder(client.UserName ?? "")
                .WithPassword(password)
                .WithTextDescription(client.TextDescription ?? "")
                .WithTextName(client.TextName ?? "");

            foreach (var role in client.Roles ?? [])
            {
                builder.AddRole(role.RoleName ?? "", role.Priority);
            }
            foreach (var group in client.Groups ?? [])
            {
                builder.AddRole(group.GroupName ?? "", group.Priority);
            }
            var result = await ExecuteCommand<GeneralResponse>(builder.Build());
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