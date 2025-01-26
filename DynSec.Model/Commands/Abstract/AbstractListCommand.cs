using System.Text.Json.Serialization;

namespace DynSec.Model.Commands.Abstract
{
    [JsonDerivedType(typeof(ListClients))]
    [JsonDerivedType(typeof(ListGroups))]
    [JsonDerivedType(typeof(ListRoles))]
    public class AbstractListCommand : AbstractCommand
    {
        public AbstractListCommand(string command) : base(command)
        {

        }

        public AbstractListCommand(string command, bool? verbose, int? count, int? offset) : base(command)
        {
            _verbose = verbose;
            _count = count;
            _offset = offset;
        }

        private readonly bool? _verbose;
        private readonly int? _count;
        private readonly int? _offset;

        public bool? Verbose { get { return _verbose; } }
        public int? Count { get { return _count; } }
        public int? Offset { get { return _offset; } }
    }
}

