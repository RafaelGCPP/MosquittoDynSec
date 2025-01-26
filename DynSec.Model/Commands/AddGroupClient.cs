using DynSec.Model.Commands.Abstract;

namespace DynSec.Model.Commands
{
    public class AddGroupClient : AbstractCommand
    {
        public AddGroupClient(string groupname, string username, int priority = -1) : base("addGroupClient")
        {
            GroupName = groupname;
            UserName = username;
            Priority = priority;
        }
        public string GroupName { get; set; }
        public string UserName { get; set; }
        public int Priority { get; set; }
    }
}
/*
{
	"commands":[
		{
			"command": "addGroupClient",
			"groupname": "group to add client to",
			"username": "client to add to group",
			"priority": -1 # Priority of the group for the client
		}
	]
}
 */