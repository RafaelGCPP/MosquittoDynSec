using DynSec.Model;
using DynSec.Model.Commands;
using DynSec.Model.Responses;
using DynSec.Model.Responses.TopLevel;
using DynSec.Protocol;
using DynSec.Protocol.Interfaces;

namespace DynSec.GraphQL
{
    public class DynSecQuery
    {
        private readonly ICLientsService clientsService;
        public DynSecQuery(ICLientsService _clientsService) { clientsService = _clientsService; }



        public async Task<ClientListData?> GetClientsListAsync(bool? verbose) => await clientsService.GetList(verbose);
        public async Task<ClientInfoData?> GetClientAsync(string client) => await clientsService.Get(client);
    }

}
