using DynSec.Model.Commands.Abstract;
using DynSec.Model.Commands.TopLevel;

namespace DynSec.Model.Commands
{
    public class ModifyGroup : AbstractCommand
    {
        public ModifyGroup(string _groupname) : base("modifyGroup")
        {
            GroupName = _groupname;
        }
        public string GroupName { get; set; }
        public string? TextName { get; set; }
        public string? TextDescription { get; set; }
        public List<RolePriority>? Roles { get; set; }

        public List<ClientPriority>? Clients { get; set; }

    }

    public class ModifyGroupBuilder : CMGroupBuilder
    {

        private readonly ModifyGroup modifyGroup;
        public ModifyGroupBuilder(string _groupname) : base(_groupname)
        {
            modifyGroup = new ModifyGroup(_groupname);
        }


        public override ModifyGroup Build()
        {
            modifyGroup.Roles = Roles;
            modifyGroup.Clients = Clients;
            modifyGroup.GroupName = groupname;
            modifyGroup.TextName = textName;
            modifyGroup.TextDescription = textDescription;

            return modifyGroup;
        }
    }
}
/*
 * {
	"commands":[
		{
			"command": "modifyGroup",
			"groupname": "group to modify",
			"textname": "", # Optional
			"textdescription": "", # Optional
			"roles": [
				{ "rolename": "role", "priority": 1 }
			], # Optional
			"clients": [
				{ "username": "client", "priority": 1 }
			] # Optional

		}
	]
}*/