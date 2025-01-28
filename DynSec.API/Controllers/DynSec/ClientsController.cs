using DynSec.Model.Responses;
using DynSec.Protocol.Exceptions;
using DynSec.Protocol.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DynSec.API.Controllers.DynSec
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ICLientsService cLientsService;

        public ClientsController(ICLientsService _cLientsService) { cLientsService = _cLientsService; }

        // GET: api/<MQTTdynsecController>/clients
        [HttpGet("clients")]
        public async Task<ActionResult<ClientListData>> GetClients(bool? verbose)
        {
            try
            {
                return Ok(await cLientsService.GetList(verbose));
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

        // GET: api/<MQTTdynsecController>/client/<client>
        [HttpGet("client/{client}")]
        public async Task<ActionResult<ClientInfoData>> GetClient(string client)
        {
            try
            {
                return Ok(await cLientsService.Get(client));
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

