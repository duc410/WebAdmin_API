using AutoFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3_WebAPIadmin.DTOs
{
    public class UserFilter
    {
        public UserFilter()
        {
        }

#nullable enable
        [FilterProperty(StringFilter = StringFilterCondition.Contains, IgnoreCase = true)]
        public string? FullName { get; set; }
        public string? Code { get; set; }
        public bool? IsEnabled { get; set; }
    }
}
