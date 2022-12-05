using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Project3_WebAPIadmin.DTOs;
using Project3_WebAPIadmin.DTOS;
using Project3_WebAPIadmin.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project3_WebAPIadmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IConfiguration _config;
        private IUserService _userService;

        public UsersController(ILogger<UsersController> logger, IConfiguration configuration, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
            _config = configuration;
        }

        // GET: api/<UsersController>
        /// <summary>
        /// Lay List Cac User
        /// </summary>
        /// <param name="userFilter"></param>
        /// <param name="paginationFilter"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetListUser([FromQuery] UserFilter userFilter, [FromQuery] PaginationFilter paginationFilter, [FromQuery] int? TenantId)
        {
            var response = await _userService.getListUser(userFilter, paginationFilter, TenantId);
            return Ok(response);
        }

        // GET api/<UsersController>/:UserId
        /// <summary>
        /// Lay User By Id
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{UserId}")]
        public IActionResult ViewUser([FromRoute] int UserId)
        {
            var response = _userService.getUserById(UserId);
            return Ok(response);
        }

        // GET: api/<UsersController>/Tenant/:UserId
        /// <summary>
        /// Lay Tenant Cua UserId do
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Tenant/{UserId}")]
        public IActionResult GetTenant([FromRoute] int UserId)
        {
            var response = _userService.getTenantByUserId(UserId);
            return Ok(response);
        }

        // POST api/<UsersController>
        /// <summary>
        /// Them User
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public IActionResult AddUser([FromQuery] int TenantId, [FromBody] CreateUserModal user)
        {
            var rs = _userService.addUser(TenantId, user);
            BaseResponse<string> res = new BaseResponse<string>();
            if (rs == 1)
            {
                res.Message = "Tạo tài khoản thành công";
                res.Succeeded = true;
            }
            else
            {
                res.Message = "Tạo tài khoản thất bại";
                res.Succeeded = false;
            }
            return Ok(res);
        }

        // PUT api/<UsersController>/5
        /// <summary>
        /// Edit User
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{UserId}")]
        public IActionResult EditUser([FromRoute] int UserId, [FromBody] CreateUserModal user)
        {
            int queryRs = _userService.editUser(UserId, user);
            BaseResponse<string> res = new BaseResponse<string>();
            if (queryRs != 0)
            {
                res.Message = "Cập nhật thành công";
            }
            else
            {
                res.Message = "Cập nhật thất bại";
                res.Succeeded = false;
            }
            return Ok(res);
        }
        /// <summary>
        /// Set Status cua User
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="isEnable"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("status/{UserId}")]
        public IActionResult SetUserStatus([FromRoute] int UserId,[FromBody] IsEnabled isEnable)
        {
            var respone = _userService.SetUserStatus(UserId, isEnable);
            return Ok(respone);
        }

        /// <summary>
        /// AssignUserToTenant
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Tenant")]
        public IActionResult AssignUserToTenant([FromBody] AssignUserToTenant data )
        {
            var respone = _userService.AssignUserToTenant(data.TenantId, data.UserId, data.action);
            return Ok(respone);
        }

        /// <summary>
        /// AssignRoleToUser
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Role")]
        public IActionResult AssignRoleToUser([FromBody] AssignRoleToUser data)
        {
            var respone = _userService.AssignRoleToUser(data.UserId, data.RoleId, data.action);
            return Ok(respone);
        }

        /// <summary>
        /// VerifyRight cua User
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="RightId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("VerifyRight")]
        public async Task<IActionResult> VerifyUserRight([FromQuery] int UserId, [FromQuery] int RightId)
        {
            var respone = await _userService.VerifyUserRight(UserId, RightId);
            return Ok(respone);
        }

        // DELETE api/<UsersController>/5
        /// <summary>
        /// Delete User by Id
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{UserId}")]
        public IActionResult DeleteUser([FromRoute] int UserId)
        {
            int deleteRs = _userService.deleteUser(UserId);
            BaseResponse<string> res = new BaseResponse<string>();
            if (deleteRs == 0)
            {
                res.Succeeded = false;
                res.Message = "Xoá không thành công";
            }
            else
            {
                res.Message = "Xoá tài khoản thành công";
            }
            return Ok(res);
        }
    }
}
