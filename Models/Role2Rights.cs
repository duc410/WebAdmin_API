using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3_WebAPIadmin.Models
{
    public class Role2Rights
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int RightId { get; set; }
        public string Name { get; set; }

        public virtual Roles Role { get; set; }

        public virtual Rights Right { get; set; }
    }
}
