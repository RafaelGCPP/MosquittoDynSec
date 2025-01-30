
using DynSec.Model;
using DynSec.Model.Responses;

namespace DynSec.Protocol.Interfaces
{
    public interface ICLientsService
    {
        Task<ClientListData?> GetList(bool? verbose);
        Task<ClientInfoData?> Get(string client);
        Task<string?> CreateClient(Client newclient, string password);
        Task<string?> DeleteClient(string client);
    }
}