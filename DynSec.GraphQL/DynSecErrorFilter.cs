using DynSec.Protocol.Exceptions;

namespace DynSec.GraphQL
{
    internal class DynSecErrorFilter : IErrorFilter
    {
        IError IErrorFilter.OnError(IError error)
        {
            IErrorBuilder eb=ErrorBuilder.FromError(error);

            eb = error.Exception switch
            {          
                DynSecProtocolInvalidParameterException ex => eb.SetMessage(ex.Message).SetCode("INVALID_PARAMETER"),
                DynSecProtocolNotFoundException ex => eb.SetMessage(ex.Message).SetCode("NOT_FOUND"),
                DynSecProtocolDuplicatedException ex => eb.SetMessage(ex.Message).SetCode("DUPLICATED"),
                DynSecProtocolTimeoutException ex => eb.SetMessage(ex.Message).SetCode("MQTT_TIMEOUT"),
                DynSecProtocolException ex => eb.SetMessage(ex.Message).SetCode("DYNAMIC_SECURITY"),
                _ => eb
            };


            return eb.Build();

        }
    }
}