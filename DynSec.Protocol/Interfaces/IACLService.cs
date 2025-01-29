using DynSec.Model;
using DynSec.Model.Responses;
using DynSec.Model.Responses.TopLevel;

namespace DynSec.Protocol.Interfaces
{
    public interface IACLService
    {
        Task<DefaultACLAccessData?> GetDefault();
        Task<String?> SetDefault(List<DefaultACL> data);
    }
}