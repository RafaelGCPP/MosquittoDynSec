using DynSec.Model.Commands.Abstract;

namespace DynSec.Model.Commands
{
    public class RemoveRoleACL : AbstractCommand
    {
        public RemoveRoleACL(string rolename, ACLType acltype, string topic) : base("removeRoleACL")
        {
            RoleName = rolename;
            ACLType = acltype;
            Topic = topic;
        }
        public string RoleName { get; set; }
        public ACLType ACLType { get; set; }
        public string Topic { get; set; }
    }
}
/*
{
	"commands":[
		{
			"command": "removeRoleACL",
			"rolename": "role",
			"acltype": "subscribePattern",
			"topic": "topic/#"
		}
	]
}
 */