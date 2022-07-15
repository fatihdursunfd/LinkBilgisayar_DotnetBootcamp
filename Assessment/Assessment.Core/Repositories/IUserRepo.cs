using Assessment.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Core.Repositories
{
    public interface IUserRepo
    {
        Task<List<UserDto>> GetAdmins();
    }
}
