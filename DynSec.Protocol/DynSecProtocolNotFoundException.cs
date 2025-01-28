
namespace DynSec.Protocol
{
    [Serializable]
    public class DynSecProtocolNotFoundException : Exception
    {
        public DynSecProtocolNotFoundException()
        {
        }

        public DynSecProtocolNotFoundException(string? message) : base(message)
        {
        }

        public DynSecProtocolNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}