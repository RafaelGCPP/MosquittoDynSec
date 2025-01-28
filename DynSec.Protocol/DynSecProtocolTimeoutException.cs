
namespace DynSec.Protocol
{
    [Serializable]
    public class DynSecProtocolTimeoutException : Exception
    {
        public DynSecProtocolTimeoutException()
        {
        }

        public DynSecProtocolTimeoutException(string? message) : base(message)
        {
        }

        public DynSecProtocolTimeoutException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}