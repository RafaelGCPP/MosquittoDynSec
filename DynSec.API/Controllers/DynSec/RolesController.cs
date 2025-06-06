﻿using DynSec.Model;
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
            catch (DynSecProtocolInvalidParameterException e)
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
            catch (DynSecProtocolInvalidParameterException e)
            {
                return StatusCode(504, e.Message);
            }
            catch (DynSecProtocolNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        // POST: api/<MQTTdynsecController>/role
        [HttpPost("role")]
        public async Task<ActionResult<string>> CreateRole(RoleACL newrole)
        {
            try
            {
                return Ok(await rolesService.CreateRole(newrole));
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

        // POST: api/<MQTTdynsecController>/role/modify
        [HttpPost("role/modify")]
        public async Task<ActionResult<string>> ModifyRole(RoleACL role)
        {
            try
            {
                return Ok(await rolesService.ModifyRole(role));
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

        // POST: api/<MQTTdynsecController>/role/<role>/delete
        [HttpPost("role/{role}/delete")]
        public async Task<ActionResult<string>> DeleteRole(string role)
        {
            try
            {
                return Ok(await rolesService.DeleteRole(role));
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

        // POST: api/<MQTTdynsecController>/role/<role>/client/<client>/add
        [HttpPost("role/{role}/client/{client}/add")]
        public async Task<ActionResult<string>> AddClientRole(string role, string client)
        {
            try
            {
                return Ok(await rolesService.AddClientRole(role, client));
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

        // POST: api/<MQTTdynsecController>/role/<role>/client/<client>/remove
        [HttpPost("role/{role}/client/{client}/remove")]
        public async Task<ActionResult<string>> RemoveClientRole(string role, string client)
        {
            try
            {
                return Ok(await rolesService.RemoveClientRole(role, client));
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

        // POST: api/<MQTTdynsecController>/role/<role>/acl/add
        [HttpPost("role/{role}/acl/add")]
        public async Task<ActionResult<string>> AddRoleACL(string role, ACLDefinition acl)
        {
            try
            {
                return Ok(await rolesService.AddRoleACL(role, acl));
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

        // POST: api/<MQTTdynsecController>/role/<role>/acl/remove
        [HttpPost("role/{role}/acl/remove")]
        public async Task<ActionResult<string>> RemoveRoleACL(string role, ACLDefinition acl)
        {
            try
            {
                return Ok(await rolesService.RemoveRoleACL(role, acl));
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

