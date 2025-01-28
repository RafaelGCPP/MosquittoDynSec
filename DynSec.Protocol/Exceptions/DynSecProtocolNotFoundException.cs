namespace DynSec.Protocol.Exceptions
{
    [Serializable]
    public class DynSecProtocolNotFoundException : DynSecProtocolException
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