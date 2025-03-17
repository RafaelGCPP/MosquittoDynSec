using DynSec.Model.Commands.Abstract;

namespace DynSec.Model.Commands.TopLevel
{
    public abstract class CMGroupBuilder
    {
        protected string groupname;
        protected string? textName;
        protected string? textDescription;
        protected List<RolePriority>? Roles;
        protected List<ClientNameClass>? Clients { get; set; }

        public CMGroupBuilder(string _groupname)
        {
            groupname = _groupname;
        }
        public CMGroupBuilder AddClient(string username)
        {
            Clients ??= new();
            Clients.Add(new ClientNameClass()
            {
                UserName = username,
            });
            return this;
        }

        public CMGroupBuilder WithTextName(string textname)
        {
            textName = textname;
            return this;
        }

        public CMGroupBuilder WithTextDescription(string textdescription)
        {
            textDescription = textdescription;
            return this;
        }
        public CMGroupBuilder AddRole(string rolename, int priority)
        {
            Roles ??= new();
            Roles.Add(new RolePriority()
            {
                RoleName = rolename,
                Priority = priority
            });
            return this;
        }
        public abstract AbstractCommand Build();

    }
}
/*
{
    "commands":[

        {
        "command": "createGroup",
			"groupname": "new group",
			"roles": [

                { "rolename": "role", "priority": 1 }
			] # Optional, roles must exist

		}
	]
}
*/