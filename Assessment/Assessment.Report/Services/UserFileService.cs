using Assessment.Core.Models;
using Assessment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Reporting.Services
{
    public class UserFileService
    {
        private readonly AppDbContext _context;

        public UserFileService(AppDbContext context)
        {
            _context = context;
        }

        public void Add(UserFile userFile)
        {
            _context.UserFiles.Add(userFile);
            _context.SaveChanges();
        }
    }
}
