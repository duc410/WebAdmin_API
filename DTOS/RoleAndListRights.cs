using Project3_WebAPIadmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3_WebAPIadmin.DTOS
{
    public class RoleAndListRights
    {
        public Roles Roles { get; set; }

        public List<Rights> Rights { get; set; }
    }
}
