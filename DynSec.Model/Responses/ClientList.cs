using DynSec.Model.Responses.Abstract;

namespace DynSec.Model.Responses
{
    public sealed class ClientList : AbstractResponse
    {



        public ClientListData? Data { get; set; }

    }

    public sealed class ClientListData : AbstractResponseData
    {
        public int? TotalCount { get; set; }
        public Client[]? Clients { get; set; }

    };


}
