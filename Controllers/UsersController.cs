using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Project3_WebAPIadmin.DTOs;
using Project3_WebAPIadmin.DTOS;
using Project3_WebAPIadmin.Repositories;
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
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetListUser([FromQuery] UserFilter userFilter, [FromQuery] PaginationFilter paginationFilter, [FromQuery] int TenantId)
        {
            var response = await _userService.getListUser(userFilter, paginationFilter, TenantId);
            return Ok(response);
        }

        // GET api/<UsersController>/5
        [HttpGet]
        [Route("{UserId}")]
        public IActionResult ViewUser([FromRoute] int UserId)
        {
            var response = _userService.getUserById(UserId);
            return Ok(response);
        }

        // POST api/<UsersController>
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

        // DELETE api/<UsersController>/5
        [HttpDelete]
        [Route("{UserId}")]
        public IActionResult deleteUser([FromRoute] int UserId)
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
