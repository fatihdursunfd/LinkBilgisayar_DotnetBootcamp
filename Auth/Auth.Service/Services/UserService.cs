using Auth.Data;
using Auth.Data.Interfaces;
using Auth.Data.Model;
using Auth.Service.Dtos;
using Auth.Service.Interfaces;
using Auth.Service.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Service.Services
{
    public class UserService : IUserService

    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly AppDbContext _context;

        private readonly IGenericRepo<UserRefreshToken> _userRefreshTokenService;

        private readonly IGenericRepo<IdentityUser> _userService;

        private readonly IUnitOfWork _unitOfWork;

        public UserService(UserManager<IdentityUser> userManager,
                                     AppDbContext context,
                                     IGenericRepo<UserRefreshToken> userRefreshTokenService,
                                     IGenericRepo<IdentityUser> userService, 
                                     IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _context = context;
            _userRefreshTokenService = userRefreshTokenService;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }


        public async Task<UserRefreshToken> AddUserRefreshTokens(UserRefreshToken userRefreshToken)
        {
            var token = await _userRefreshTokenService.AddAsync(userRefreshToken);
            return token;
        }

        public async void CreateUser(UserDto user)
        {
            IdentityUser newUser = new IdentityUser();

            newUser.UserName = user.Name.ToLower();
            newUser.Email = "fd@gmail.com";
            newUser.PasswordHash = user.Password;

            await _userService.AddAsync(newUser);

            SaveCommit();
        }

        public async Task<Response<NoData>> CreateUserAsync(UserDto user)
        {
            IdentityUser newUser = new IdentityUser();

            newUser.UserName = user.Name.ToLower();
            newUser.Email = user.Email;

            var result = await _userManager.CreateAsync(newUser, user.Password);

            if (!result.Succeeded)
            {
                List<string> errors = new List<string>();
                foreach (var error in result.Errors)
                    errors.Add(error.Description);
                var errorsString = errors.Aggregate((a, b) => a + "\n" + b);
                return new Response<NoData>() { Data = null, Error = errorsString, StatusCode = 400 };

            }
            return new Response<NoData>() { Data = null , Error = "" , StatusCode = 200};
        }

        public void DeleteUserRefreshTokens(string username, string refreshToken)
        {
            var item = _userRefreshTokenService.Where(x => x.UserName == username && x.RefreshToken == refreshToken).FirstOrDefault(); 
            if (item != null)
                _userRefreshTokenService.Remove(item);
        }

        public UserRefreshToken GetSavedRefreshTokens(string username, string refreshtoken)
        {
            return _userRefreshTokenService
                     .Where(x => x.UserName == username && x.RefreshToken == refreshtoken && x.IsActive == true)
                     .FirstOrDefault();  

        }

        public async Task<List<UserDto>> GetUsers()
        {
            var users = await _userService.GetAllAsync();

            var usersDto = users.Select(x => new UserDto() 
            { 
                Name = x.UserName, 
                Email = x.Email, 
                Password = x.PasswordHash} 
            ).ToList();

            return usersDto;
        }

        public async Task<bool> IsValidUserAsync(LoginDto loginDto)
        {
            var user = _userManager.Users.FirstOrDefault(o => o.UserName == loginDto.Name);
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            return result;
        }

        public void SaveCommit()
        {
            _unitOfWork.Commit();
        }
    }
}
