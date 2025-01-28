using DynSec.Model.Commands;
using DynSec.Model.Commands.Abstract;
using DynSec.Model.Responses;
using DynSec.Protocol.Interfaces;

namespace DynSec.Protocol
{
    public class GroupsService : BaseService, IGroupsService
    {

        public GroupsService(IDynamicSecurityHandler _handler) : base(_handler) { }

        public async Task<GroupListData?> GetList(bool? verbose)
        {
            var cmd = new ListGroups(verbose ?? true);
            var result = await ExecuteCommand<GroupList>(cmd);
            return result.Data;
        }

        public async Task<GroupInfoData?> Get(string group)
        {
            var cmd = new GetGroup(group);
            var result = await ExecuteCommand<GroupInfo>(cmd);
            return result.Data;
        }

        public async Task<AnonymousGroupInfoData?> GetAnonymous()
        {
            var cmd = new GetAnonymousGroup();
            var result = await ExecuteCommand<AnonymousGroupInfo>(cmd);
            return result.Data;
        }

    }
}
