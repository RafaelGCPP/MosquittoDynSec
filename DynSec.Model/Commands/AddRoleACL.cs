using DynSec.Model.Commands.Abstract;

namespace DynSec.Model.Commands
{
    public class AddRoleACL : AbstractCommand
    {
        public AddRoleACL(string rolename, ACLType acltype, string topic, int priority = -1, bool allow = true) : base("addRoleACL")
        {
            RoleName = rolename;
            ACLType = acltype;
            Topic = topic;
            Priority = priority;
            Allow = allow;
        }
        public string RoleName { get; set; }
        public ACLType ACLType { get; set; }
        public string Topic { get; set; }
        public int Priority { get; set; }
        public bool Allow { get; set; }
    }
}
/*
{
	"commands":[
		{
			"command": "addRoleACL",
			"rolename": "role",
			"acltype": "subscribePattern",
			"topic": "topic/#",
			"priority": -1,
			"allow": true
		}
	]
}
 */