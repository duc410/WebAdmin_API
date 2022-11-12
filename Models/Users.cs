using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3_WebAPIadmin.Models
{
    public class Users
    {
        public Users()
        {
            User2Roles = new HashSet<User2Roles>();
        }

        public int Id { get; set; }
        public int TenantId { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Code { get; set; }
        public string Note { get; set; }
        public bool IsEnabled { get; set; }

        public virtual Tenants Tenant { get; set; }

        public virtual ICollection<User2Roles> User2Roles { get; set; }


    }
}
