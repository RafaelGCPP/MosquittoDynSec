namespace DynSec.Protocol.Exceptions
{
    [Serializable]
    public class DynSecProtocolException : Exception
    {
        public DynSecProtocolException()
        {
        }

        public DynSecProtocolException(string? message) : base(message)
        {
        }

        public DynSecProtocolException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}