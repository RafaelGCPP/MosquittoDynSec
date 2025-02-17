namespace DynSec.Protocol.Exceptions
{
    [Serializable]
    public class DynSecProtocolDuplicatedException : DynSecProtocolException
    {
        public DynSecProtocolDuplicatedException()
        {
        }

        public DynSecProtocolDuplicatedException(string? message) : base(message)
        {
        }

        public DynSecProtocolDuplicatedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}