using DynSec.Model.Commands.Abstract;
using DynSec.Model.Commands.TopLevel;

namespace DynSec.Model.Commands
{

    public class ModifyClient : AbstractCommand
    {
        public ModifyClient(string _username) : base("modifyClient")
        {
            UserName = _username;
        }

        public string UserName { get; set; }
        public string? Password { get; set; }
        public string? TextName { get; set; }
        public string? TextDescription { get; set; }

        public List<RolePriority>? Roles { get; set; }
        public List<GroupPriority>? Groups { get; set; }
    }

    public class ModifyClientBuilder : CMClientBuilder
    {
        public ModifyClientBuilder(string _username)
        {
            UserName = _username;
        }

        protected string UserName { get; set; }
        protected string? Password { get; set; }

        public ModifyClientBuilder WithPassword(string? _password) { Password = _password; return this; }
        public override ModifyClient Build()
        {
            return new ModifyClient(UserName)
            {
                Password = Password,
                TextName = TextName,
                TextDescription = TextDescription,
                Roles = Roles,
                Groups = Groups,
            };
        }

    }
}