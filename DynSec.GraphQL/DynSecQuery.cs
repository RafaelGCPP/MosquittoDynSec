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
        public async Task<ClientListData?> GetClientListAsync(bool? verbose, IDynamicSecurityHandler dynSec)
        {
            var cmd = new ListClients(verbose ?? true);
            var result = await dynSec.ExecuteCommand(cmd) ?? new GeneralResponse
            {
                Error = "Task cancelled",
                Command = cmd.Command,
                Data = null
            };

            switch (result.Error)
            {
                case "Ok":
                    var data = ((ClientList)result).Data;
                    return data;
                case "Task cancelled":
                    return null;
                default:
                    return null;
            }
        }
    }

}
