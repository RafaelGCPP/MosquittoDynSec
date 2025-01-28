using DynSec.Model.Commands;
using DynSec.Model.Commands.Abstract;
using DynSec.Model.Responses;
using DynSec.Model.Responses.Abstract;
using DynSec.Model.Responses.TopLevel;
using DynSec.Protocol.Exceptions;
using DynSec.Protocol;
using DynSec.Protocol.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DynSec.API.Controllers.DynSec
{

    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupsService groupService;
        private readonly IDynamicSecurityHandler dynSec;

        public GroupsController(IGroupsService _groupService, IDynamicSecurityHandler _dynSec) { groupService = _groupService; dynSec = _dynSec; }

        // GET: api/<MQTTdynsecController>/groups
        [HttpGet("groups")]
        public async Task<ActionResult<GroupListData>> GetGroups(bool? verbose)
        {
            try
            {
                return Ok(await groupService.GetList(verbose));
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

        // GET: api/<MQTTdynsecController>/group/<group>
        [HttpGet("group/{group}")]
        public async Task<ActionResult<GroupInfoData>> GetGroup(string group)
        {
            try
            {
                return Ok(await groupService.Get(group));
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

        // GET: api/<MQTTdynsecController>/anonymous-group
        [HttpGet("anonymous-group")]
        public async Task<ActionResult<AnonymousGroupInfoData>> GetAnonymousGroups()
        {
            try
            {
                return Ok(await groupService.GetAnonymous());
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

        // POST: api/<MQTTdynsecController>/anonymous-group
        [HttpPost("anonymous-group")]
        public async Task<ActionResult<GeneralResponse>> SetAnonymousGroups([FromBody] string group)
        {
            var cmd = new SetAnonymousGroup(group);

            var result = await dynSec.ExecuteCommand(cmd) ?? new GeneralResponse
            {
                Error = "Task cancelled",
                Command = cmd.Command,
                Data = null
            };
            switch (result.Error)
            {
                case "Ok":
                    return Ok();
                case "Task cancelled":
                    return StatusCode(504);
                default:
                    return NotFound(result);
            }
        }
    }
}

