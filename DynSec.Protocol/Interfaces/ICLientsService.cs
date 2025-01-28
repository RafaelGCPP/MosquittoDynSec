
using DynSec.Model.Responses;

namespace DynSec.Protocol.Interfaces
{
    public interface ICLientsService
    {
        Task<ClientListData?> GetList(bool? verbose);
        Task<ClientInfoData?> Get(string client);
    }
}