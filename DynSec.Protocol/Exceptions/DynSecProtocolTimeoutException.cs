namespace DynSec.Protocol.Exceptions
{
    [Serializable]
    public class DynSecProtocolTimeoutException : DynSecProtocolException
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