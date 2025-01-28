using DynSec.Model.Responses;

namespace DynSec.Protocol.Interfaces
{
    public interface IGroupsService
    {
        Task<GroupListData?> GetList(bool? verbose);
        Task<GroupInfoData?> Get(string group);
        Task<AnonymousGroupInfoData?> GetAnonymous();
    }
}