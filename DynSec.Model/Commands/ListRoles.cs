using DynSec.Model.Commands.Abstract;

namespace DynSec.Model.Commands
{
    public sealed class ListRoles : AbstractListCommand
    {
        public ListRoles() : base("listRoles") { }
        public ListRoles(bool? verbose = false, int? count = null, int? offset = null)
            : base("listRoles", verbose, count, offset) { }

    }
}
