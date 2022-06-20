using Auth.Data.Model;
using Auth.Service.Dtos;
using Auth.Service.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Service.Interfaces
{
    public interface IUserService
    {
		Task<bool> IsValidUserAsync(LoginDto loginDto);

		Task<UserRefreshToken> AddUserRefreshTokens(UserRefreshToken user);

		UserRefreshToken GetSavedRefreshTokens(string username, string refreshtoken);

		void DeleteUserRefreshTokens(string username, string refreshToken);

		void CreateUser(UserDto user);

		Task<Response<NoData>> CreateUserAsync(UserDto user);

		Task<List<UserDto>> GetUsers();

		void SaveCommit();
	}
}
