namespace DynSec.Model.Commands.Abstract
{
    public sealed class GetGroup : AbstractCommand
    {
        public GetGroup(string groupname) : base("getGroup") { _groupname = groupname; }
        private readonly string _groupname;
        public string Groupname { get { return _groupname; } }
    }
}
