using DynSec.Model.Commands;
using DynSec.Model.Responses;
using DynSec.Protocol.Interfaces;

namespace DynSec.Protocol
{
    public partial class ClientsService(IDynamicSecurityRpc _handler) : BaseService(_handler), ICLientsService
    {
        public async Task<ClientListData?> GetList(bool? verbose)
        {
            var cmd = new ListClients(verbose ?? true);
            var result = await ExecuteCommand<ClientList>(cmd);
            return result.Data;
        }

        public async Task<ClientInfoData?> Get(string client)
        {
            var cmd = new GetClient(client);
            var result = await ExecuteCommand<ClientInfo>(cmd);
            return result.Data;
        }
    }
}
