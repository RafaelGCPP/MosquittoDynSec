using DynSec.Model.Commands.Abstract;

namespace DynSec.Model.Commands.TopLevel
{
    public abstract class CMClientBuilder
    {

        protected string? TextName { get; set; }
        protected string? TextDescription { get; set; }

        protected List<RolePriority>? Roles { get; set; }
        protected List<GroupPriority>? Groups { get; set; }


        public CMClientBuilder WithTextName(string _textName) { TextName = _textName; return this; }
        public CMClientBuilder WithTextDescription(string _textDescription) { TextDescription = _textDescription; return this; }
        public CMClientBuilder AddRole(string roleName, int priority)
        {
            Roles ??= new();
            Roles.Add(new RolePriority
            {
                RoleName = roleName,
                Priority = priority,
            });
            return this;
        }

        public CMClientBuilder AddEmptyRoleSet()
        {
            Roles ??= new();
            return this;
        }
        public CMClientBuilder AddGroup(string groupName, int priority)
        {
            Groups ??= new();
            Groups.Add(new GroupPriority
            {
                GroupName = groupName,
                Priority = priority
            });
            return this;
        }

        public CMClientBuilder AddEmptyGroupSet()
        {
            Groups ??= new();
            return this;
        }

        public abstract AbstractCommand Build();
    }
}
