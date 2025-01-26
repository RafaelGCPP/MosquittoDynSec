using DynSec.Model.Commands.Abstract;

namespace DynSec.Model.Commands
{
    public class DeleteGroup : AbstractCommand
    {
        public DeleteGroup(string groupname) : base("deleteGroup") { _groupname = groupname; }
        private readonly string _groupname;
        public string GroupName { get { return _groupname; } }
    }
}
