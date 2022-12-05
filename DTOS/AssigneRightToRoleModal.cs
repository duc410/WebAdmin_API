using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3_WebAPIadmin.DTOS
{
    public class AssigneRightToRoleModal
    {
        public int RoleId { get; set; }
        public int RightId { get; set; }
        public bool action { get; set; }
    }
}
