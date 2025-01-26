using DynSec.Model.Responses.Abstract;

namespace DynSec.Model.Responses
{
    public sealed class AnonymousGroupInfo : AbstractResponse
    {

        public AnonymousGroupInfoData? Data { get; set; }
    }

    public sealed class AnonymousGroupInfoData : AbstractResponseData
    {
        public GroupNameClass? Group { get; set; }
    }
}
