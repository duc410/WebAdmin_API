using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3_WebAPIadmin.Models
{
    public class User2Roles
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Name  { get; set; }

        public virtual Users User { get; set; }

        public virtual Roles Role { get; set; }

    }
}
