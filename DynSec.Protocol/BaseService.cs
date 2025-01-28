using DynSec.Protocol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynSec.Protocol
{
    public class BaseService
    {
        protected readonly IDynamicSecurityHandler dynSec;
        protected BaseService(IDynamicSecurityHandler _handler) { dynSec = _handler; }
        protected void RaiseError(string? error)
        {
            if (error == "Task Cancelled") throw new DynSecProtocolTimeoutException(error);
            throw new DynSecProtocolNotFoundException(error);
        }
    }
}
