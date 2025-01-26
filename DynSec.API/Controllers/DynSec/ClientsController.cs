using DynSec.Model.Commands;
using DynSec.Model.Responses;
using DynSec.Model.Responses.TopLevel;
using DynSec.Protocol.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DynSec.API.Controllers.DynSec
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IDynamicSecurityProtocol dynSec;

        public ClientsController(IDynamicSecurityProtocol _dynSec) { dynSec = _dynSec; }

        // GET: api/<MQTTdynsecController>/clients
        [HttpGet("clients")]
        public async Task<ActionResult<ClientListData>> GetClients(bool? verbose)
        {
            var cmd = new ListClients(verbose ?? true);
            var result = await dynSec.ExecuteCommand(cmd) ?? new GeneralResponse
            {
                Error = "Task cancelled",
                Command = cmd.Command,
                Data = null
            };

            switch (result.Error)
            {
                case "Ok":
                    var data = ((ClientList)result).Data;
                    return Ok(data);
                case "Task cancelled":
                    return StatusCode(504);
                default:
                    return NotFound(result);
            }

        }

        // GET: api/<MQTTdynsecController>/client/<client>
        [HttpGet("client/{client}")]
        public async Task<ActionResult<ClientInfoData>> GetClient(string client)
        {
            var cmd = new GetClient(client);
            var result = await dynSec.ExecuteCommand(cmd) ?? new GeneralResponse
            {
                Error = "Task cancelled",
                Command = cmd.Command,
                Data = null
            };

            switch (result.Error)
            {
                case "Ok":
                    var data = ((ClientInfo)result).Data;
                    return Ok(data);
                case "Task cancelled":
                    return StatusCode(504);
                default:
                    return NotFound(result);
            }
        }



    }
}

