using DynSec.Model.Responses;
using DynSec.Protocol.Exceptions;
using DynSec.Protocol.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DynSec.API.Controllers.DynSec
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService rolesService;

        public RolesController(IRolesService _rolesService) { rolesService = _rolesService; }

        // GET: api/<MQTTdynsecController>/roles
        [HttpGet("roles")]
        public async Task<ActionResult<RoleListData>> GetRoles(bool? verbose)
        {
            try
            {
                return Ok(await rolesService.GetList(verbose));
            }
            catch (DynSecProtocolTimeoutException e)
            {
                return StatusCode(504, e.Message);
            }
            catch (DynSecProtocolNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        // GET: api/<MQTTdynsecController>/role/<role>
        [HttpGet("role/{role}")]
        public async Task<ActionResult<RoleInfoData>> GetRole(string role)
        {
            try
            {
                return Ok(await rolesService.Get(role));
            }
            catch (DynSecProtocolTimeoutException e)
            {
                return StatusCode(504, e.Message);
            }
            catch (DynSecProtocolNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}

