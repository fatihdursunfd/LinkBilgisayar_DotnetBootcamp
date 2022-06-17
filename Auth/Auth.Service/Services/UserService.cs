using Auth.Data.Interfaces;
using Auth.Service.Dtos;
using Auth.Service.Interfaces;
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
        private readonly IUnitOfWork unitOfWork;
        public UserService(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Response<UserDto>> CreateUserAsync(CreateUserDto model)
        {
            var user = new IdentityUser() { Email = model.Email , UserName = model.UserName };
            var result = await _userManager.CreateAsync(user , model.Password);
            
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return Response<UserDto>.Fail(new ErrorDto(errors, true), 400);
            }

            unitOfWork.Commit();

            return Response<UserDto>.Success(new UserDto() { Id = user.Id , Email = user.Email , UserName= user.UserName } , 200);

        }

        public async Task<Response<UserDto>> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
                return Response<UserDto>.Fail("UserName not found", 404, true);

            return Response<UserDto>.Success(new UserDto() { Id = user.Id, Email = user.Email, UserName = user.UserName }, 200);
        }
    }
}
