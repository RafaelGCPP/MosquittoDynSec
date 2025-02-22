namespace DynSec.Protocol.Exceptions
{
    [Serializable]
    public class DynSecProtocolInvalidParameterException : DynSecProtocolException
    {
        public DynSecProtocolInvalidParameterException()
        {
        }

        public DynSecProtocolInvalidParameterException(string? message) : base(message)
        {
        }

        public DynSecProtocolInvalidParameterException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}