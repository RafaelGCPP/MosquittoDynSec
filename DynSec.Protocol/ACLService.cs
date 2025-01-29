using DynSec.Model;
using DynSec.Model.Commands;
using DynSec.Model.Responses;
using DynSec.Model.Responses.TopLevel;
using DynSec.Protocol.Interfaces;

namespace DynSec.Protocol
{
    public class ACLService(IDynamicSecurityHandler _handler) : BaseService(_handler), IACLService
    {
        public async Task<DefaultACLAccessData?> GetDefault()
        {
            var cmd = new GetDefaultACLAccess();
            var result = await ExecuteCommand<DefaultACLAccess>(cmd);
            return result.Data;
        }

        public async Task<String?> SetDefault(List<DefaultACL> data)
        {
            var cmd = new SetDefaultACLAccess(data);
            var result = await ExecuteCommand<GeneralResponse>(cmd);
            return commandDoneString;
        }
    }
}

