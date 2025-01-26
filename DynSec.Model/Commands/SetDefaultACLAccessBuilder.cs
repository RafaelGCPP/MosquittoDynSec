using DynSec.Model.Commands.Abstract;

namespace DynSec.Model.Commands
{


    public class SetDefaultACLAccess : AbstractCommand
    {
        public SetDefaultACLAccess() : base("setDefaultACLAccess") { }
        public SetDefaultACLAccess(List<DefaultACL> _ACLs) : base("setDefaultACLAccess") { ACLs = _ACLs; }

        public List<DefaultACL> ACLs { get; set; } = new List<DefaultACL>();

    }


}
