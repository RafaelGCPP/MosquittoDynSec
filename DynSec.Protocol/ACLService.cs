using DynSec.Model.Commands;
using DynSec.Model.Responses;
using DynSec.Protocol.Interfaces;

namespace DynSec.Protocol
{
    public class ACLService : BaseService, IACLService
    {
        public ACLService(IDynamicSecurityHandler _handler) : base(_handler) { }
        public async Task<DefaultACLAccessData?> GetDefault()
        {
            var cmd = new GetDefaultACLAccess();
            var result = await ExecuteCommand<DefaultACLAccess>(cmd);
            return result.Data;
        }
    }
}

