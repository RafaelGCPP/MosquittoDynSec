using DynSec.Model.Responses.Abstract;

namespace DynSec.Model.Responses
{
    public class RoleInfo : AbstractResponse
    {
        public RoleInfoData? Data { get; set; }
    }

    public class RoleInfoData : AbstractResponseData
    {
        public RoleACL? Role { get; set; }
    }
}


