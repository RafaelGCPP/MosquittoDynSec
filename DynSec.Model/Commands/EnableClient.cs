using DynSec.Model.Commands.Abstract;

namespace DynSec.Model.Commands
{
    public class EnableClient : AbstractCommand
    {
        public EnableClient(string username) : base("enableClient") { _username = username; }
        private readonly string _username;
        public string Username { get { return _username; } }
    }
}
