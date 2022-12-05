using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3_WebAPIadmin.DTOS
{
    public class CreateRoleModal
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }

        public List<int> UserIds { get; set; }
        public List<int> RightIds { get; set; }
    }
}
