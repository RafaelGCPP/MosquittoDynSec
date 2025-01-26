using DynSec.Model.Responses.Abstract;

namespace DynSec.Model.Responses
{
    public sealed class DefaultACLAccess : AbstractResponse
    {

        public DefaultACLAccessData? Data { get; set; }
    }
    public sealed class DefaultACLAccessData : AbstractResponseData
    {
        public DefaultACL[]? ACLs { get; set; }
    }
}
