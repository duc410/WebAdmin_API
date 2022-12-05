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

        public async Task<PagedResponse<List<UserRespone>>> getListUser(UserFilter userFilter, PaginationFilter paginationFilter,int? TenantId)
        {
            var page = paginationFilter.Page;
            var record = paginationFilter.Record;
            var query = _db.Users.AutoFilter(userFilter);
            if (TenantId.HasValue)
            {
                query = query.Where(item => item.TenantId == TenantId);

            }
            var total = await _db.Users
                        .AutoFilter(userFilter).CountAsync();
            List<UserRespone> userList = new List<UserRespone>();
            if (paginationFilter.Paging == true)
            {
                userList = await query.Include(item=>item.Tenant)
                                      .Include(item=>item.User2Roles)
                                      .ThenInclude(item=>item.Role)
                                      .ThenInclude(item=>item.Role2Rights)
                                      .ThenInclude(item=>item.Right)
                    .Skip((page - 1) * record).Take(record)
                    .Select(x => new UserRespone
                    {
                        Id = x.Id,
                        TenantName = x.Tenant.Name,
                        TenantId = x.TenantId,
                        FullName = x.FullName,
                        Code = x.Code,
                        Note = x.Note,
                        IsEnabled = x.IsEnabled,
                        RoleAndListRights = GetRoleAndListRightByRoleId(x.User2Roles.Select(item => item.Role.Id).ToList()),
                    })
                    .ToListAsync();
            }
            else
            {
                userList = await query.Include(item => item.Tenant)
                                      .Include(item => item.User2Roles)
                                      .ThenInclude(item => item.Role)
                                      .ThenInclude(item => item.Role2Rights)
                                      .ThenInclude(item => item.Right)
                     .Select(x => new UserRespone
                     {
                         Id = x.Id,
                         TenantName = x.Tenant.Name,
                         TenantId = x.TenantId,
                         FullName = x.FullName,
                         Code = x.Code,
                         Note = x.Note,
                         IsEnabled = x.IsEnabled,
                         RoleAndListRights = GetRoleAndListRightByRoleId(x.User2Roles.Select(item => item.Role.Id).ToList()),
                     })
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

        public Tenants getTenantByUserId(int UserId)
        {
            Tenants tenant=_db.Users.Where(item => item.Id==UserId)
                              .Include(u=>u.Tenant)
                              .Select(x=>new Tenants { Id=x.TenantId,Name=x.Tenant.Name})
                              .FirstOrDefault();


            return tenant;
        }
        //Todo :implement controller 
        public int SetUserStatus(int UserId, IsEnabled isEnable)
        {
            var editUser = _db.Users.Where(item => item.Id == UserId).FirstOrDefault();
            editUser.IsEnabled = isEnable.isEnabled;
            return _db.SaveChanges();
        }

        public int AssignUserToTenant(int TenantId, int UserId, bool action)
        {
            var user = _db.Users.Where(item => item.Id == UserId).FirstOrDefault();
            if (action)
            {
                user.TenantId = TenantId;
                return _db.SaveChanges();
            }
            else
            {
                user.IsEnabled = false;
                return _db.SaveChanges();
            }
        }

        public int AssignRoleToUser(int UserId, int RoleId, bool action)
        {
            var user = _db.User2Roles.Where(item => item.Id == UserId).FirstOrDefault();
            if (action)
            {
                user.RoleId = RoleId;
                return _db.SaveChanges();
            }
            else
            {
                _db.User2Roles.Remove(user);
                return _db.SaveChanges();
            }
        }

        public async Task<bool> VerifyUserRight(int UserId, int RightId)
        {
            var user = _db.Users.Where(item => item.Id == UserId);
            List<User2Roles> User2Roles = (List<User2Roles>)user.Select(item => item.User2Roles).FirstOrDefault();
            List<int> roleIds = new List<int>();
            foreach(User2Roles User2Role in User2Roles)
            {
                roleIds.Add(User2Role.RoleId);
            }
            foreach(int roleId in roleIds)
            {
                var right = await _db.Role2Rights.Where(item => item.RoleId == roleId).Include(a => a.Right).Select(item => item.RightId).ToListAsync();
                if (right.Contains(RightId))
                    return true;
            }
            return false;
                                
        }


        //Util function
        private List<RoleAndListRights> GetRoleAndListRightByRoleId(List<int> roleIds)
        {
            List<RoleAndListRights> result = new List<RoleAndListRights>();
            foreach (int roleId in roleIds)
            {
                Roles role = _db.Roles.Where(item => item.Id == roleId).FirstOrDefault();
                List<Rights> rights = _db.Role2Rights.Where(item => item.RoleId == roleId)
                    .Include(item => item.Right).Select(item => item.Right).ToList();

                result.Add(new RoleAndListRights() { Roles = role, Rights = rights });
            }
            return result;
        }

    }
}
