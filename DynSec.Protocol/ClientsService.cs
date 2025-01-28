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
    public class ClientsService : BaseService, ICLientsService
    {
        public ClientsService(IDynamicSecurityHandler _handler) : base(_handler) { }

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
