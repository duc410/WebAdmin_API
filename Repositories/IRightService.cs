using Project3_WebAPIadmin.DTOS;
using Project3_WebAPIadmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3_WebAPIadmin.Repositories
{
    public interface IRightService
    {
        public Task<List<Rights>> GetRights(int? RoleId);
        public int AddRight(int? RoleId, CreateRightModal Right);

        public Rights GetRight(int RightId);

        public int DeleteRight(int RightId);

        public int SetRightStatus(int RightId, IsEnabled isEnable);

        public int AssignRightToRole(int RoleId, int RightId, bool action);
    }
}
