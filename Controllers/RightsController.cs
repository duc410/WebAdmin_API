using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Project3_WebAPIadmin.DTOS;
using Project3_WebAPIadmin.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3_WebAPIadmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RightsController : ControllerBase
    {
        private readonly ILogger<RightsController> _logger;
        private readonly IConfiguration _config;
        private IRightService _rightService;

        public RightsController(ILogger<RightsController> logger, IConfiguration configuration, IRightService rightService)
        {
            _logger = logger;
            _rightService = rightService;
            _config = configuration;
        }

        // GET: api/<UsersController>/:RoleId
        /// <summary>
        /// Get Right by Id
        /// </summary>
        /// <param name="RightId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{RightId}")]
        public IActionResult GetRight([FromRoute] int RightId)
        {
            var response = _rightService.GetRight(RightId);

            return Ok(response);
        }

        // GET: api/<UsersController>/Role?RoleId=
        /// <summary>
        /// Get Tat ca Right hoac Right cua 1 Role
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Role")]
        public async Task<IActionResult> GetRights([FromQuery] int? RoleId = null)
        {
            var respone = await _rightService.GetRights(RoleId);
            return Ok(respone);
        }
        /// <summary>
        /// Them Right
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="addRight"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public IActionResult AddRight([FromQuery] int? RoleId, [FromBody] CreateRightModal addRight)
        {
            var respone = _rightService.AddRight(RoleId, addRight);
            return Ok(respone);
        }

        /// <summary>
        /// Set Status cua Right
        /// </summary>
        /// <param name="RightId"></param>
        /// <param name="IsEnable"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{RightId}")]
        public IActionResult SetRightStatus([FromRoute] int RightId, [FromBody] IsEnabled IsEnable)
        {
            var respone = _rightService.SetRightStatus(RightId, IsEnable);
            return Ok(respone);
        }

        /// <summary>
        ///  AssignRightToRole
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Role")]
        public IActionResult AssignRightToRole([FromBody] AssigneRightToRoleModal data)
        {
            var respone = _rightService.AssignRightToRole(data.RoleId, data.RightId, data.action);
            return Ok(respone);
        }

        /// <summary>
        /// Xoa right By Id
        /// </summary>
        /// <param name="RightId"></param>
        /// <returns></returns>
        // DELETE api/<RolesController>/5
        [HttpDelete]
        [Route("{RightId}")]
        public IActionResult DeleteRight([FromRoute] int RightId)
        {
            var respone = _rightService.DeleteRight(RightId);
            return Ok(respone);
        }
    }
}
