using DynSec.Model;
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
        private readonly ICLientsService clientsService;

        public ClientsController(ICLientsService _cLientsService) { clientsService = _cLientsService; }

        // GET: api/<MQTTdynsecController>/clients
        [HttpGet("clients")]
        public async Task<ActionResult<ClientListData>> GetClients(bool? verbose)
        {
            try
            {
                return Ok(await clientsService.GetList(verbose));
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
                return Ok(await clientsService.Get(client));
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

        // POST: api/<MQTTdynsecController>/client
        [HttpPost("client")]
        public async Task<ActionResult<string>> CreateClient(Client newclient, string password)
        {
            try
            {
                return Ok(await clientsService.CreateClient(newclient, password));
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

        // PUT: api/<MQTTdynsecController>/client
        [HttpPut("client")]
        public async Task<ActionResult<string>> ModifyClient(Client client, string? password)
        {
            try
            {
                return Ok(await clientsService.ModifyClient(client, password));
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

        // DELETE: api/<MQTTdynsecController>/client/<client>
        [HttpDelete("client/{client}")]
        public async Task<ActionResult<string>> DeleteClient(string client)
        {
            try
            {
                return Ok(await clientsService.DeleteClient(client));
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

