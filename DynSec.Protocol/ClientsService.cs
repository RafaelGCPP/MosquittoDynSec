using DynSec.Model.Commands;
using DynSec.Model.Responses.TopLevel;
using DynSec.Model.Responses;
using DynSec.Protocol.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using DynSec.Model;

namespace DynSec.Protocol
{
    public class ClientsService : ICLientsService
    {
        private readonly IDynamicSecurityHandler dynSec;
        public ClientsService(IDynamicSecurityHandler _handler) { dynSec = _handler; }
        private void RaiseError(string? error)
        {
            if (error == "Task Cancelled") throw new DynSecProtocolTimeoutException(error);
            throw new DynSecProtocolNotFoundException(error);
        }

        public async Task<ClientListData?> GetList(bool? verbose)
        {
            var cmd = new ListClients(verbose ?? true);
            var result = await dynSec.ExecuteCommand(cmd) ?? new GeneralResponse
            {
                Error = "Task cancelled",
                Command = cmd.Command,
                Data = null
            };

            if (result.Error == "Ok") return ((ClientList)result).Data;
             
            RaiseError(result.Error);
            return null;
        }

        public async Task<ClientInfoData?> Get(string username)
        {
            var cmd = new GetClient(client);
            var result = await dynSec.ExecuteCommand(cmd) ?? new GeneralResponse
            {
                Error = "Task cancelled",
                Command = cmd.Command,
                Data = null
            };

            if (result.Error == "Ok") return ((ClientInfo)result).Data;

            RaiseError(result.Error);
            return null;
        }
    }
}
