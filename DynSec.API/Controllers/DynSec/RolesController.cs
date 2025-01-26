using DynSec.Model.Commands;
using DynSec.Model.Responses;
using DynSec.Model.Responses.Abstract;
using DynSec.Model.Responses.TopLevel;
using DynSec.Protocol.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DynSec.API.Controllers.DynSec 
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IDynamicSecurityProtocol dynSec;

        public RolesController(IDynamicSecurityProtocol _dynSec) { dynSec = _dynSec; }

        // GET: api/<MQTTdynsecController>/roles
        [HttpGet("roles")]
        public async Task<ActionResult<RoleListData>> GetRoles(bool? verbose)
        {
            var cmd = new ListRoles(verbose ?? true);
            AbstractResponse result = await dynSec.ExecuteCommand(cmd) ?? new GeneralResponse
            {
                Error = "Task cancelled",
                Command = cmd.Command,
                Data = null
            };

            switch (result.Error)
            {
                case "Ok":
                    var data = ((RoleList)result).Data;
                    return Ok(data);
                case "Task cancelled":
                    return StatusCode(504);
                default:
                    return NotFound(result);
            }
        }

        // GET: api/<MQTTdynsecController>/role/<role>
        [HttpGet("role/{role}")]
        public async Task<ActionResult<RoleInfoData>> GetRole(string role)
        {
            var cmd = new GetRole(role);
            AbstractResponse result = await dynSec.ExecuteCommand(cmd) ?? new GeneralResponse
            {
                Error = "Task cancelled",
                Command = cmd.Command,
                Data = null
            };

            switch (result.Error)
            {
                case "Ok":
                    var data = ((RoleInfo)result).Data;
                    return Ok(data);
                case "Task cancelled":
                    return StatusCode(504);
                default:
                    return NotFound(result);
            }
        }
    }
}

