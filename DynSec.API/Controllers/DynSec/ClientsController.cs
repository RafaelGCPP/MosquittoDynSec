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
            catch (DynSecProtocolInvalidParameterException e)
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
            catch (DynSecProtocolInvalidParameterException e)
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
            catch (DynSecProtocolInvalidParameterException e)
            {
                return StatusCode(504, e.Message);
            }
            catch (DynSecProtocolNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        // POST: api/<MQTTdynsecController>/client/modify
        [HttpPost("client/modify")]
        public async Task<ActionResult<string>> ModifyClient(Client client, string? password)
        {
            try
            {
                return Ok(await clientsService.ModifyClient(client, password));
            }
            catch (DynSecProtocolInvalidParameterException e)
            {
                return StatusCode(504, e.Message);
            }
            catch (DynSecProtocolNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        // POST: api/<MQTTdynsecController>/client/<client>/delete
        [HttpPost("client/{client}/delete")]
        public async Task<ActionResult<string>> DeleteClient(string client)
        {
            try
            {
                return Ok(await clientsService.DeleteClient(client));
            }
            catch (DynSecProtocolInvalidParameterException e)
            {
                return StatusCode(504, e.Message);
            }
            catch (DynSecProtocolNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        // POST: api/<MQTTdynsecController>/client/<client>/enable
        [HttpPost("client/{client}/enable")]
        public async Task<ActionResult<string>> EnableClient(string client)
        {
            try
            {
                return Ok(await clientsService.EnableClient(client));
            }
            catch (DynSecProtocolInvalidParameterException e)
            {
                return StatusCode(504, e.Message);
            }
            catch (DynSecProtocolNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        // POST: api/<MQTTdynsecController>/client/<client>/disable

        [HttpPost("client/{client}/disable")]
        public async Task<ActionResult<string>> DisableClient(string client)
        {
            try
            {
                return Ok(await clientsService.DisableClient(client));
            }
            catch (DynSecProtocolInvalidParameterException e)
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

