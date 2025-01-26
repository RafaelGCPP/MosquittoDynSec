using DynSec.Model.Commands.Abstract;
using DynSec.Model.Commands.TopLevel;

namespace DynSec.Model.Commands
{
    public class ModifyRole : AbstractCommand
    {
        public ModifyRole(string _roleName) : base("modifyRole")
        {
            RoleName = _roleName;
        }

        public string? RoleName { get; set; }
        public string? TextName { get; set; }
        public string? TextDescription { get; set; }
        public List<ACLDefinition>? ACLs { get; set; }
    }
    public class ModifyRoleBuilder : CMRoleBuilder
    {
        private readonly ModifyRole modifyRole;
        public ModifyRoleBuilder(string _roleName)
        {
            modifyRole = new ModifyRole(_roleName);
        }
        public override ModifyRole Build()
        {
            modifyRole.TextDescription = TextDescription;
            modifyRole.TextName = TextName;
            modifyRole.ACLs = ACLs;

            return modifyRole;
        }
    }
}
