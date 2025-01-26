using DynSec.Model.Commands.Abstract;

namespace DynSec.Model.Commands
{
    public class RemoveClientRole : AbstractCommand
    {
        public RemoveClientRole(string username, string rolename) : base("removeClientRole")
        {
            UserName = username;
            RoleName = rolename;
        }
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }
}
/*
{
	"commands":[
		{
			"command": "removeClientRole",
			"username": "client to remove role from",
			"rolename": "role to remove"
		}
	]
}
 */