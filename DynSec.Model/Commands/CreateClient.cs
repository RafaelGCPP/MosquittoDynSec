using DynSec.Model.Commands.Abstract;
using DynSec.Model.Commands.TopLevel;

namespace DynSec.Model.Commands
{
    public class CreateClient : AbstractCommand
    {
        public CreateClient(string _username, string _password) : base("createClient")
        {
            UserName = _username;
            Password = _password;
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string? TextName { get; set; }
        public string? TextDescription { get; set; }

        public List<RolePriority>? Roles { get; set; }
        public List<GroupPriority>? Groups { get; set; }

    }

    public class CreateClientBuilder : CMClientBuilder
    {
        public CreateClientBuilder(string _username, string _password)
        {
            UserName = _username;
            Password = _password;
        }
        protected string UserName { get; set; }

        protected string Password { get; set; }

        public override CreateClient Build()
        {
            return new CreateClient(UserName, Password)
            {
                TextName = TextName,
                TextDescription = TextDescription,
                Roles = Roles,
                Groups = Groups,
            };
        }
    }
}
