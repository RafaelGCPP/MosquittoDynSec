using DynSec.Model;
using DynSec.Model.Commands;
using DynSec.Model.Responses;
using DynSec.Model.Responses.TopLevel;
using DynSec.Protocol;
using DynSec.Protocol.Exceptions;
using DynSec.Protocol.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DynSec.API.Controllers.DynSec
{

    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController(IGroupsService _groupService) : ControllerBase
    {
        private readonly IGroupsService groupService = _groupService;

        // GET: api/<MQTTdynsecController>/groups
        [HttpGet("groups")]
        public async Task<ActionResult<GroupListData>> GetGroups(bool? verbose)
        {
            try
            {
                return Ok(await groupService.GetList(verbose));
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

        // GET: api/<MQTTdynsecController>/group/<group>
        [HttpGet("group/{group}")]
        public async Task<ActionResult<GroupInfoData>> GetGroup(string group)
        {
            try
            {
                return Ok(await groupService.Get(group));
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

        // GET: api/<MQTTdynsecController>/anonymous-group
        [HttpGet("anonymous-group")]
        public async Task<ActionResult<AnonymousGroupInfoData>> GetAnonymousGroups()
        {
            try
            {
                return Ok(await groupService.GetAnonymous());
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

        // POST: api/<MQTTdynsecController>/anonymous-group
        [HttpPost("anonymous-group")]
        public async Task<ActionResult<GeneralResponse>> SetAnonymousGroups([FromBody] string group)
        {
            try
            {
                return Ok(await groupService.SetAnonymous(group));
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

        // POST: api/<MQTTdynsecController>/group
        [HttpPost("group")]
        public async Task<ActionResult<string>> CreateGroup(Group newgroup)
        {
            try
            {
                return Ok(await groupService.CreateGroup(newgroup));
            }
            catch (DynSecProtocolInvalidParameterException e)
            {
                return StatusCode(504, e.Message);
            }
            catch (DynSecProtocolDuplicatedException e)
            {
                return StatusCode(409, e.Message);
            }
        }


        // POST: api/<MQTTdynsecController>/group/modify
        [HttpPost("group/modify")]
        public async Task<ActionResult<string>> ModifyGroup(Group group)
        {
            try
            {
                return Ok(await groupService.ModifyGroup(group));
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

        // POST: api/<MQTTdynsecController>/group/<group>/delete
        [HttpPost("group/{group}/delete")]
        public async Task<ActionResult<string>> DeleteGroup(string group)
        {
            try
            {
                return Ok(await groupService.DeleteGroup(group));
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

