using DynSec.Model;
using DynSec.Model.Responses;
using DynSec.Protocol.Exceptions;
using DynSec.Protocol.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DynSec.API.Controllers.DynSec
{
    [Route("api/[controller]")]
    [ApiController]
    public class ACLsController : ControllerBase
    {
        private readonly IACLService aclsService;

        public ACLsController(IACLService _aclsService) { aclsService = _aclsService; }

        // GET: api/<MQTTdynsecController>/defaultACL
        [HttpGet("defaultACL")]
        public async Task<ActionResult<DefaultACLAccessData>> GetDefaultACL()
        {
            try
            {
                return Ok(await aclsService.GetDefault());
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
        [HttpPost("defaultACL")]
        public async Task<ActionResult<String>> SetDefaultACL([FromBody] List<DefaultACL> data)
        {
            try
            {
                return Ok(await aclsService.SetDefault(data));
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

