using DynSec.Model.Commands.Abstract;

namespace DynSec.Model.Commands
{
    public sealed class GetDefaultACLAccess : AbstractCommand
    {
        public GetDefaultACLAccess() : base("getDefaultACLAccess") { }
    }
}
