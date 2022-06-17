using Auth.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Service.Interfaces
{
    public interface IUserService
    {
        Task<Response<UserDto>> CreateUserAsync(CreateUserDto model);

        Task<Response<UserDto>> GetUserByNameAsync(string userName);
    }
}
