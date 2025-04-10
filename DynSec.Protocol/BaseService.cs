﻿using DynSec.Model.Commands.Abstract;
using DynSec.Model.Responses.Abstract;
using DynSec.Model.Responses.TopLevel;
using DynSec.Protocol.Exceptions;
using DynSec.Protocol.Interfaces;

namespace DynSec.Protocol
{
    public class BaseService
    {
        protected const string commandDoneString = "Done";

        protected readonly IDynamicSecurityRpc dynSec;
        protected BaseService(IDynamicSecurityRpc _handler) { dynSec = _handler; }
        protected DynSecProtocolException SelectException(string? error)
        {
            if (error is null) return new DynSecProtocolException(error);

            if (error == "Task Cancelled") return new DynSecProtocolTimeoutException(error);
            if (error.Contains("not found")) return new DynSecProtocolNotFoundException(error);
            if (error.Contains("already exists")) return new DynSecProtocolDuplicatedException(error);

            return new DynSecProtocolException(error);
        }

        protected async Task<T> ExecuteCommand<T>(AbstractCommand cmd) where T : AbstractResponse
        {
            var result = await dynSec.ExecuteCommand(cmd) ?? new GeneralResponse
            {
                Error = "Task cancelled",
                Command = cmd.Command,
                Data = null
            };
            if (result.Error == "Ok") return (T)result;
            throw SelectException(result.Error);
        }
    }
}
