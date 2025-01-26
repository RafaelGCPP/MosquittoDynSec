using DynSec.Model.Responses.Abstract;

namespace DynSec.Model.Responses
{
    public sealed class GroupList : AbstractResponse
    {

        public GroupListData? Data { get; set; }
    }
    public sealed class GroupListData : AbstractResponseData
    {
        public int? TotalCount { get; set; }
        public Group[]? Groups { get; set; }
    }
}
