using DynSec.Model.Responses.Abstract;

namespace DynSec.Model.Responses
{
    public sealed class GroupInfo : AbstractResponse
    {


        public GroupInfoData? Data { get; set; }
    }

    public sealed class GroupInfoData : AbstractResponseData
    {
        public Group? Group { get; set; }
    }
}
