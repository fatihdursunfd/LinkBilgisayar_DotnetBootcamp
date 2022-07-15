using Assessment.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Core.Services
{
    public interface IUserService
    {
        Task CreateUser(UserDto userDto);

        Task MakeAdmin(string email);

        Task AddRole(string roleName);

        Task<List<UserGetDto>> GetAdmins();
    }
}
