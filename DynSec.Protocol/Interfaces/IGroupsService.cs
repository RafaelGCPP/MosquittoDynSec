using DynSec.Model;
using DynSec.Model.Responses;

namespace DynSec.Protocol.Interfaces
{
    public interface IGroupsService
    {
        Task<GroupListData?> GetList(bool? verbose);
        Task<GroupInfoData?> Get(string group);
        Task<AnonymousGroupInfoData?> GetAnonymous();
        Task<string?> CreateGroup(Group newgroup);
        Task<string?> ModifyGroup(Group newgroup);
        Task<string?> DeleteGroup(string group);


    }
}