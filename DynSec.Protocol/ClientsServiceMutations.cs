using DynSec.Model;
using DynSec.Model.Commands;
using DynSec.Model.Responses.TopLevel;
using DynSec.Protocol.Interfaces;

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
                builder.AddRole(role.RoleName ?? "", 0);
            }
            foreach (var role in newclient.Groups ?? [])
            {
                builder.AddRole(role.GroupName ?? "", 0);
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
    }
}