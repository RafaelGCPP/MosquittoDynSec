using DynSec.Model;
using DynSec.Model.Commands;
using DynSec.Model.Responses;
using DynSec.Model.Responses.TopLevel;
using DynSec.Protocol;
using DynSec.Protocol.Interfaces;

namespace DynSec.GraphQL
{
    public  class DynSecQuery
    {
        public async Task<ClientListData?> GetClientsListAsync(bool? verbose, ICLientsService clientsService)
        {
            try
            {
                return await clientsService.GetList(verbose);
            }
            catch (DynSecProtocolTimeoutException e)
            {
                return null;
            }
            catch (DynSecProtocolNotFoundException e)
            {
                return null;
            }
        }

        public async Task<ClientInfoData?> GetClientAsync(string client, ICLientsService cLientsService)
        {
            try
            {
                return await cLientsService.Get(client);
            }
            catch (DynSecProtocolTimeoutException e)
            {
                return null;
            }
            catch (DynSecProtocolNotFoundException e)
            {
                return null;
            }
        }
    }

}
