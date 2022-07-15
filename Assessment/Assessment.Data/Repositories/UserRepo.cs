using Assessment.Core.DTOs;
using Assessment.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Data.Repositories
{
    public class UserRepo : IUserRepo
    {
        public Task<List<UserDto>> GetAdmins()
        {
            throw new NotImplementedException();
        }
    }
}
