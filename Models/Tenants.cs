using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3_WebAPIadmin.Models
{
    public class Tenants
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Users> Users { get; set; }

        public virtual ICollection<Roles> Roles { get; set; }
    }
}
