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
            var result = await ExecuteCommand<RoleList>(cmd);
            return result.Data;
        }

        public async Task<RoleInfoData?> Get(string role)
        {
            var cmd = new GetRole(role);
            var result = await ExecuteCommand<RoleInfo>(cmd);
            return result.Data;
        }

    }
}
