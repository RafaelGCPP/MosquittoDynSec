using DynSec.Model.Commands.Abstract;

namespace DynSec.Model.Commands
{
    public class AddClientRole : AbstractCommand
    {
        public AddClientRole(string username, string rolename, int priority = -1) : base("addClientRole")
        {
            UserName = username;
            RoleName = rolename;
            Priority = priority;
        }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public int Priority { get; set; }
    }
}
/*
{
	"commands":[
		{
			"command": "addClientRole",
			"username": "client to add role to",
			"rolename": "role to add",
			"priority": -1 # Optional priority
		}
	]
}
 */