using Project3_WebAPIadmin.DTOS;
using Project3_WebAPIadmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3_WebAPIadmin.Repositories
{
    public interface IRoleService
    {
        public Task<List<Roles>> GetRoles(int? TenantId);
        public int AddRole(int? TenantId ,CreateRoleModal role);

        public Roles GetRole(int RoleId);

        public int DeleteRole(int RoleId);

        public int SetRoleStatus(int RoleId, IsEnabled isEnable);

        public int AssignRoleToTenant(int TenantId, int RoleId, bool action);
    }
}
