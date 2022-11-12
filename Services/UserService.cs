using AutoFilter;
using Microsoft.EntityFrameworkCore;
using Project3_WebAPIadmin.DTOs;
using Project3_WebAPIadmin.DTOS;
using Project3_WebAPIadmin.Models;
using Project3_WebAPIadmin.Repositories;
using Project3_WebAPIadmin.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3_WebAPIadmin.Services
{
    public class UserService : IUserService
    {
        private readonly AdminDbContext _db;

        public UserService(AdminDbContext db)
        {
            _db = db;
        }

        public async Task<PagedResponse<List<UserRespone>>> getListUser(UserFilter userFilter, PaginationFilter paginationFilter,int TenantId)
        {
            var page = paginationFilter.Page;
            var record = paginationFilter.Record;
            var query = _db.Users.AutoFilter(userFilter).Where(item=>item.TenantId==TenantId);
            var total = await _db.Users
                        .AutoFilter(userFilter).CountAsync();
            List<UserRespone> userList = new List<UserRespone>();
            if (paginationFilter.Paging == true)
            {
                userList = await query.Skip((page - 1) * record).Take(record)
                    .Select(x => new UserRespone { Id = x.Id, TenantId=x.TenantId,FullName=x.FullName,
                        Code=x.Code,Note=x.Note,IsEnabled=x.IsEnabled })
                    .ToListAsync();
            }
            else
            {
                userList = await query
                    .Select(x => new UserRespone { Id = x.Id, TenantId = x.TenantId, FullName = x.FullName,
                        Code = x.Code, Note = x.Note, IsEnabled = x.IsEnabled })
                    .ToListAsync();
            }
            return new PagedResponse<List<UserRespone>>(userList, total);
        }

        public UserRespone getUserById(int UserId)
        {
            var user=_db.Users.Where(item=> item.Id==UserId)
                    .Select(x => new UserRespone
                    {
                        Id = x.Id,
                        TenantId = x.TenantId,
                        FullName = x.FullName,
                        Code = x.Code,
                        Note = x.Note,
                        IsEnabled = x.IsEnabled
                    })
                    .FirstOrDefault();
            return user;
        }

        public int addUser(int TenantId, CreateUserModal userAddinfo)
        {
            var createdUser = new Users() {TenantId=TenantId,  FullName = userAddinfo.FullName, Password = BCrypt.Net.BCrypt.HashPassword(userAddinfo.Password),
                Code= userAddinfo.Code,Note= userAddinfo.Note,IsEnabled= userAddinfo.IsEnabled ,
            };
            _db.Users.Add(createdUser);
            _db.SaveChanges();
            foreach (int RoleId in userAddinfo.RoleIds)
            {
                User2Roles user2Role = new User2Roles() { UserId = createdUser.Id, RoleId = RoleId };
                _db.User2Roles.Add(user2Role);
            }
            return _db.SaveChanges();
        }

        public int editUser(int UserId, CreateUserModal userEditInfo)
        {
            var editUser = _db.Users.Where(item => item.Id == UserId).FirstOrDefault();
            userEditInfo.Password = BCrypt.Net.BCrypt.HashPassword(userEditInfo.Password);
            var udpateRs = EntityUtils.updateRecord(editUser, userEditInfo);
            return _db.SaveChanges();
        }

        public int deleteUser(int UserId)
        {
            var editUser = _db.Users.Where(item => item.Id == UserId).FirstOrDefault();
            _db.Users.Remove(editUser);
            return _db.SaveChanges();
        }
    }
}
