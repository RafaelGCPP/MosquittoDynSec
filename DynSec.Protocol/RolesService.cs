using DynSec.Model.Commands;
using DynSec.Model.Responses.Abstract;
using DynSec.Model.Responses.TopLevel;
using DynSec.Model.Responses;
using DynSec.Protocol.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynSec.Protocol
{
    public class RolesService : BaseService, IRolesService
    {
        public RolesService(IDynamicSecurityHandler _handler) : base(_handler) { }


        public async Task<RoleListData?> GetList(bool? verbose)
        {
            var cmd = new ListRoles(verbose ?? true);
            AbstractResponse result = await dynSec.ExecuteCommand(cmd) ?? new GeneralResponse
            {
                Error = "Task cancelled",
                Command = cmd.Command,
                Data = null
            };

            if (result.Error == "Ok") return ((RoleList)result).Data;
            RaiseError(result.Error);
            return null;
        }

        public async Task<RoleInfoData?> Get(string role)
        {
            var cmd = new GetRole(role);
            AbstractResponse result = await dynSec.ExecuteCommand(cmd) ?? new GeneralResponse
            {
                Error = "Task cancelled",
                Command = cmd.Command,
                Data = null
            };

            if (result.Error == "Ok") return ((RoleInfo)result).Data;
            RaiseError(result.Error);
            return null;
        }

    }
}
