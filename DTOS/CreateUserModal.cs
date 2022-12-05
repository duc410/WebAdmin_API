using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3_WebAPIadmin.DTOS
{
    public class CreateUserModal
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Code { get; set; }
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? Note { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public bool IsEnabled { get; set; }

        public List<int> RoleIds { get; set; }
    }
}
