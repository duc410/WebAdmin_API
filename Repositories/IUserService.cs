using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project3_WebAPIadmin.DTOs;
using Project3_WebAPIadmin.DTOS;
using Project3_WebAPIadmin.Models;

namespace Project3_WebAPIadmin.Repositories
{
    public interface IUserService
    {
        public Task<PagedResponse<List<UserRespone>>> getListUser(UserFilter userFilter, PaginationFilter paginationFilter,int TenantId);
        public UserRespone getUserById(int UserId);
        public int addUser(int TenantId, CreateUserModal user);
        public int editUser(int UserId, CreateUserModal user);
        public int deleteUser(int userId);
    }
}
