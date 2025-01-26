using DynSec.Model.Responses.Abstract;

namespace DynSec.Model.Responses
{
    public sealed class ClientInfo : AbstractResponse
    {
        public ClientInfoData? Data { get; set; }
    }

    public sealed class ClientInfoData : AbstractResponseData
    {
        public Client? Client { get; set; }
    }
}
