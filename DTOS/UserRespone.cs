using Project3_WebAPIadmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3_WebAPIadmin.DTOS
{
    public class UserRespone
    {
        public int Id { get; set; }
        public int TenantId { get; set; }

        public string TenantName { get; set; }
        public string FullName { get; set; }
        public string Code { get; set; }
        public string Note { get; set; }
        public bool IsEnabled { get; set; }

        public List<RoleAndListRights> RoleAndListRights { get; set; }
    }
}
