﻿using DynSec.Model.Commands.Abstract;
using DynSec.Model.Commands.TopLevel;
using DynSec.Model.Responses.Abstract;
using DynSec.Model.Responses.TopLevel;

namespace DynSec.Protocol.Interfaces
{

    public interface IDynamicSecurityRpc
    {
        Task<AbstractResponse> ExecuteCommand(AbstractCommand cmd);
    }
}


