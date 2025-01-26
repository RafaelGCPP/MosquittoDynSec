using System.Text.Json.Serialization;

namespace DynSec.Model.Commands.Abstract
{
    [JsonDerivedType(typeof(AbstractListCommand))]
    [JsonDerivedType(typeof(SetDefaultACLAccess))]
    [JsonDerivedType(typeof(GetDefaultACLAccess))]
    [JsonDerivedType(typeof(CreateClient))]
    [JsonDerivedType(typeof(DeleteClient))]
    [JsonDerivedType(typeof(EnableClient))]
    [JsonDerivedType(typeof(DisableClient))]
    [JsonDerivedType(typeof(GetClient))]
    [JsonDerivedType(typeof(ListClients))]
    [JsonDerivedType(typeof(ModifyClient))]
    [JsonDerivedType(typeof(SetClientId))]
    [JsonDerivedType(typeof(SetClientPassword))]
    [JsonDerivedType(typeof(AddClientRole))]
    [JsonDerivedType(typeof(RemoveClientRole))]
    [JsonDerivedType(typeof(AddGroupClient))]
    [JsonDerivedType(typeof(CreateGroup))]
    [JsonDerivedType(typeof(DeleteGroup))]
    [JsonDerivedType(typeof(GetGroup))]
    [JsonDerivedType(typeof(ListGroups))]
    [JsonDerivedType(typeof(ModifyGroup))]
    [JsonDerivedType(typeof(RemoveGroupClient))]
    [JsonDerivedType(typeof(AddGroupRole))]
    [JsonDerivedType(typeof(RemoveGroupRole))]
    [JsonDerivedType(typeof(SetAnonymousGroup))]
    [JsonDerivedType(typeof(GetAnonymousGroup))]
    [JsonDerivedType(typeof(CreateRole))]
    [JsonDerivedType(typeof(GetRole))]
    [JsonDerivedType(typeof(ListRoles))]
    [JsonDerivedType(typeof(ModifyRole))]
    [JsonDerivedType(typeof(DeleteRole))]
    [JsonDerivedType(typeof(AddRoleACL))]
    //RemoveRoleACL  
    public abstract class AbstractCommand
    {
        public AbstractCommand(string command) { _command = command; }
        private readonly string _command = "";
        public string Command { get { return _command; } }


    }
}
