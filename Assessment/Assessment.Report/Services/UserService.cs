using Assessment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Reporting.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public List<string> GetAdmins()
        {
            var adminId = _context.Roles.Where(x => x.Name == "admin").FirstOrDefault().Id;
            var adminUserIds = _context.UserRoles.Where(x => x.RoleId == adminId).Select(x => x.UserId).ToList();
            var admins = _context.Users.Where(x => adminUserIds.Contains(x.Id)).ToList();

            return admins.Select(x => x.Email).ToList();
        }
    }
}
