using DynSec.Model.Commands.Abstract;

namespace DynSec.Model.Commands
{
    public class DeleteClient : AbstractCommand
    {
        public DeleteClient(string username) : base("deleteClient") { _username = username; }
        private readonly string _username;
        public string Username { get { return _username; } }
    }
}
