﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3_WebAPIadmin.DTOS
{
    public class AssignUserToTenant
    {
        public int TenantId { get; set; }
        public int UserId { get; set; }
        public bool action { get; set; }
    }
}
