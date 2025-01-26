using DynSec.Model.Commands.Abstract;
using DynSec.Model.Commands.TopLevel;

namespace DynSec.Model.Commands
{
    public class CreateRole : AbstractCommand
    {
        public CreateRole(string _roleName) : base("createRole")
        {
            RoleName = _roleName;
        }
        public string? RoleName { get; set; }
        public string? TextName { get; set; }
        public string? TextDescription { get; set; }
        public List<ACLDefinition>? ACLs { get; set; }
    }

    public class CreateRoleBuilder : CMRoleBuilder
    {
        private readonly CreateRole createRole;
        public CreateRoleBuilder(string _roleName)
        {
            createRole = new CreateRole(_roleName);
        }
        public override CreateRole Build()
        {
            createRole.TextDescription = TextDescription;
            createRole.TextName = TextName;
            createRole.ACLs = ACLs;

            return createRole;
        }
    }
}
/*
 * {
	"commands":[
		{
			"command": "createRole",
			"rolename": "new role",
			"textname": "", # Optional
			"textdescription": "", # Optional
			"acls": [
				{ "acltype": "subscribePattern", "topic": "topic/#", "priority": -1, "allow": true}
			] # Optional
		}
	]
}*/