using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3_WebAPIadmin.Models
{
    public class Roles
    {
        public Roles()
        {
            User2Roles = new HashSet<User2Roles>();
            Role2Rights = new HashSet<Role2Rights>();
        }

        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }

        public virtual Tenants Tenant { get; set; }

        public virtual ICollection<User2Roles> User2Roles { get; set; }

        public virtual ICollection<Role2Rights> Role2Rights { get; set; }
    }
}
