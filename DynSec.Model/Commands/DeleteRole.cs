using DynSec.Model.Commands.Abstract;

namespace DynSec.Model.Commands
{
    public class DeleteRole : AbstractCommand
    {
        public DeleteRole(string rolename) : base("deleteRole") { _rolename = rolename; }
        private readonly string _rolename;
        public string RoleName { get { return _rolename; } }
    }
}
