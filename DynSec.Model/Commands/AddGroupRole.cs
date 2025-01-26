using DynSec.Model.Commands.Abstract;

namespace DynSec.Model.Commands
{
    public class AddGroupRole : AbstractCommand
    {
        public AddGroupRole(string groupname, string rolename, int priotrity = -1) : base("addGroupRole")
        {
            GroupName = groupname;
            RoleName = rolename;
            Priority = priotrity;
        }
        public string GroupName { get; set; }
        public string RoleName { get; set; }
        public int Priority { get; set; }
    }
}
/*
{
	"commands":[
		{
			"command": "addGroupRole",
			"groupname": "group to add role to",
			"rolename": "role to add",
			"priority": -1 # Optional priority
		}
	]
}
 */