using Microsoft.EntityFrameworkCore;
using Project3_WebAPIadmin.DTOS;
using Project3_WebAPIadmin.Models;
using Project3_WebAPIadmin.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project3_WebAPIadmin.Services
{
    public class RightService : IRightService
    {
        private readonly AdminDbContext _db;

        public RightService(AdminDbContext db)
        {
            _db = db;
        }
        public int AddRight(int? RoleId, CreateRightModal Right)
        {
            var createdRight = new Rights();
            if (RoleId.HasValue)
            {
                createdRight = new Rights() { Name = Right.Name, Description = Right.Description, IsEnabled = Right.IsEnabled 
                };

            }
            else
            {
                //ToDo: eroor when tenantId null
                createdRight = new Rights() { Name = Right.Name, Description = Right.Description, IsEnabled = Right.IsEnabled, };
            }
            _db.Rights.Add(createdRight);
            _db.SaveChanges();
            foreach (int roleId in Right.RoleIds)
            {
                Role2Rights role2right = new Role2Rights() { RoleId= roleId, RightId= createdRight.Id };
                _db.Role2Rights.Add(role2right);
            }
            return _db.SaveChanges();
        }

        public int AssignRightToRole(int RoleId, int RightId, bool action)
        {
            var role2right = _db.Role2Rights.Where(item => item.RoleId == RoleId).ToList();
            if (action)
            {
                if (role2right.Any(item => item.RightId == RightId))
                    return 1;
                else
                {
                    Role2Rights role2Rights = new Role2Rights() { RightId = RightId, RoleId = RoleId };
                    _db.Role2Rights.Add(role2Rights);
                    return _db.SaveChanges();
                }
                    
            }
            else
            {
                if (role2right.Any(item => item.RightId == RightId))
                {
                    _db.Role2Rights.RemoveRange((IEnumerable<Role2Rights>)role2right.Select(item => item.RightId == RightId).ToList());
                    return _db.SaveChanges();
                }
                else return 1;
                 
            }
        }

        public int DeleteRight(int RightId)
        {
            var editRight = _db.Rights.Where(item => item.Id == RightId).FirstOrDefault();
            _db.Rights.Remove(editRight);
            return _db.SaveChanges();
        }

        public Rights GetRight(int RightId)
        {
            Rights right = _db.Rights.Where(item => item.Id == RightId).FirstOrDefault();
            return right;
        }

        public async Task<List<Rights>> GetRights(int? RoleId)
        {
            if (RoleId.HasValue)
            {
                var rights = await _db.Role2Rights.Where(item => item.Id == RoleId)
                    .Include(item=>item.Right).Select(item=>item.Right)
                                       .ToListAsync();
                return rights;
            }
            else
            {
                var rights = await _db.Rights
                                      .ToListAsync();
                return rights;
            }
        }

        public int SetRightStatus(int RightId, IsEnabled isEnable)
        {
            var editRight = _db.Rights.Where(item => item.Id == RightId).FirstOrDefault();
            editRight.IsEnabled = isEnable.isEnabled;
            return _db.SaveChanges();
        }
    }
}
