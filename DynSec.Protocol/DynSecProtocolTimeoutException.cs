
namespace DynSec.Protocol
{
    [Serializable]
    internal class DynSecProtocolTimeoutException : Exception
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