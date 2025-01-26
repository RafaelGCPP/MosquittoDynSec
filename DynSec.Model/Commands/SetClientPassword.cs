using DynSec.Model.Commands.Abstract;

namespace DynSec.Model.Commands
{
    public class SetClientPassword : AbstractCommand
    {
        public SetClientPassword(string username, string password) : base("setClientPassword")
        {
            UserName = username;
            Password = password;
        }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
/*
{

    "commands":[

        {
        "command": "setClientPassword",
			"username": "username to change",
			"password": "new password"

        }
	]
}
*/