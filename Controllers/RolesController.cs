using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Project3_WebAPIadmin.DTOS;
using Project3_WebAPIadmin.Models;
using Project3_WebAPIadmin.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3_WebAPIadmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ILogger<RolesController> _logger;
        private readonly IConfiguration _config;
        private IRoleService _roleService;

        public RolesController(ILogger<RolesController> logger, IConfiguration configuration, IRoleService roleService)
        {
            _logger = logger;
            _roleService = roleService;
            _config = configuration;
        }

        // GET: api/<RolesController>/:RoleId
        /// <summary>
        /// Lay Role by Id
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{RoleId}")]
        public IActionResult GetRole([FromRoute] int RoleId)
        {
            var response = _roleService.GetRole(RoleId);

            return Ok(response);
        }

        /// <summary>
        /// Lay Tat ca role hoac Role cua TenantId
        /// </summary>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Tenant")]
        public async Task<IActionResult> GetRoles([FromQuery] int? TenantId=null)
        {
            var respone = await _roleService.GetRoles(TenantId);
            return Ok(respone);
        }

        /// <summary>
        /// Them Role
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="addRole"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public IActionResult AddRole([FromQuery] int? TenantId,[FromBody] CreateRoleModal addRole)
        {
            var respone = _roleService.AddRole(TenantId, addRole);
            return Ok(respone);
        }

        /// <summary>
        /// Set Status cua Role
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="IsEnable"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{RoleId}")]
        public IActionResult SetRoleStatus([FromRoute] int RoleId, [FromBody] IsEnabled IsEnable)
        {
            var respone = _roleService.SetRoleStatus(RoleId, IsEnable);
            return Ok(respone);
        }


        /// <summary>
        /// AssignRoleToTenant
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Tenant")]
        public IActionResult AssignRoleToTenant([FromBody] AssignRoleToTenantModal data)
        {
            var respone = _roleService.AssignRoleToTenant(data.TenantId,data.RoleId, data.action);
            return Ok(respone);
        }

        /// <summary>
        /// Xoa Role by ID
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        // DELETE api/<RolesController>/5
        [HttpDelete]
        [Route("{RoleId}")]
        public IActionResult DeleteRole([FromRoute] int RoleId)
        {
            var respone = _roleService.DeleteRole(RoleId);
            return Ok(respone);
        }
    }
}