using DynSec.Model.Responses;

namespace DynSec.Protocol.Interfaces
{
    public interface IACLService
    {
        Task<DefaultACLAccessData?> GetDefault();
    }
}