using DynSec.Model.Commands.Abstract;

namespace DynSec.Model.Commands
{
    public class SetAnonymousGroup : AbstractCommand
    {
        public SetAnonymousGroup(string groupname) : base("setAnonymousGroup")
        {
            GroupName = groupname;
        }
        public string GroupName { get; set; }
    }
}
/*
{
	"commands":[
		{
			"command": "setAnonymousGroup",
			"groupname": "group"
		}
	]
}
 */