using Microsoft.EntityFrameworkCore;
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
    public class RoleService : IRoleService
    {
        private readonly AdminDbContext _db;

        public RoleService(AdminDbContext db)
        {
            _db = db;
        }

        public int AssignRoleToTenant(int TenantId, int RoleId, bool action)
        {
            var role = _db.Roles.Where(item => item.Id == RoleId).FirstOrDefault();
            if (action)
            {
                role.TenantId = TenantId;
                return _db.SaveChanges();
            }
            else
            {
                role.IsEnabled = false;
                return _db.SaveChanges();
            }
        }

        public int DeleteRole(int RoleId)
        {
            var editRole = _db.Roles.Where(item => item.Id == RoleId).FirstOrDefault();
            editRole.IsEnabled = false;
            return _db.SaveChanges();
        }

        public Roles GetRole(int RoleId)
        {
            Roles role = _db.Roles.Where(item => item.Id == RoleId).FirstOrDefault();
            return role;
        }

        public async  Task<List<Roles>> GetRoles(int? TenantId)
        {
            if (TenantId.HasValue)
            {
                var roles  = await _db.Roles.Where(item => item.TenantId == TenantId)
                                       .ToListAsync();
                return roles;
            }
            else
            {
                var roles = await _db.Roles
                                      .ToListAsync();
                return roles;
            }

        }

        public int SetRoleStatus(int RoleId, IsEnabled isEnable)
        {
            var editRole = _db.Roles.Where(item => item.Id == RoleId).FirstOrDefault();
            editRole.IsEnabled = isEnable.isEnabled;
            return _db.SaveChanges();
        }

        int IRoleService.AddRole(int? TenantId, CreateRoleModal addRole)
        {
            var createdRole = new Roles();
            if (TenantId.HasValue)
            {
                createdRole = new Roles() { TenantId = (int)TenantId, Name = addRole.Name, Description = addRole.Description, IsEnabled = addRole.IsEnabled };

            }
            else
            {
                //ToDo: eroor when tenantId null
                createdRole = new Roles() { /*TenantId=null*/Name = addRole.Name, Description = addRole.Description, IsEnabled = addRole.IsEnabled };
            }
            _db.Roles.Add(createdRole);
            _db.SaveChanges();
            foreach (int userId in addRole.UserIds)
            {
                User2Roles user2Role = new User2Roles() { UserId = userId, RoleId = createdRole.Id };
                _db.User2Roles.Add(user2Role);
            }
            foreach (int rightId in addRole.RightIds)
            {
                Role2Rights role2Right = new Role2Rights() { RightId = rightId, RoleId = createdRole.Id };
                _db.Role2Rights.Add(role2Right);
            }
            return _db.SaveChanges();
        }
    }
}
