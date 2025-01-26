using DynSec.Model.Commands.Abstract;

namespace DynSec.Model.Commands
{
    public sealed class ListClients : AbstractListCommand
    {
        public ListClients() : base("listClients") { }
        public ListClients(bool verbose = false, int? count = null, int? offset = null)
            : base("listClients", verbose, count, offset) { }
    }
}
