using DynSec.Model.Commands.Abstract;

namespace DynSec.Model.Commands
{
    public class RemoveGroupRole : AbstractCommand
    {
        public RemoveGroupRole(string groupname, string rolename) : base("removeGroupRole")
        {
            GroupName = groupname;
            RoleName = rolename;
        }
        public string GroupName { get; set; }
        public string RoleName { get; set; }
    }
}
/*
{
	"commands":[
		{
			"command": "removeGroupRole",
			"groupname": "group",
			"rolename": "role"
		}
	]
}
 */