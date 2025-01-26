using DynSec.Model.Commands.Abstract;

namespace DynSec.Model.Commands
{
    public class DisableClient : AbstractCommand
    {
        public DisableClient(string username) : base("disableClient") { _username = username; }
        private readonly string _username;
        public string Username { get { return _username; } }
    }
}
