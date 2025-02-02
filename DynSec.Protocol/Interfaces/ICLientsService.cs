
using DynSec.Model;
using DynSec.Model.Commands;
using DynSec.Model.Responses;
using DynSec.Model.Responses.TopLevel;

namespace DynSec.Protocol.Interfaces
{
    public interface ICLientsService
    {
        Task<ClientListData?> GetList(bool? verbose);
        Task<ClientInfoData?> Get(string client);
        Task<string?> CreateClient(Client newclient, string password);
        Task<string?> DeleteClient(string client);
        Task<string?> ModifyClient(Client client, string? password);
        Task<string?> EnableClient(string client);
        Task<string?> DisableClient(string client);
    }
}