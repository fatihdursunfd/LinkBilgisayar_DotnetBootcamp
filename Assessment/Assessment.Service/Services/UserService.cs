using Assessment.Core.DTOs;
using Assessment.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<IdentityUser> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task AddRole(string roleName)
        {
            await _roleManager.CreateAsync(new IdentityRole() { Name = roleName });
        }

        public async Task MakeAdmin(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            await _userManager.AddToRoleAsync(user, "admin");
        }

        public async Task CreateUser(UserDto userDto)
        {
            var user = _mapper.Map<IdentityUser>(userDto);
            await _userManager.CreateAsync(user, userDto.Password);
            await _userManager.AddToRoleAsync(user, "editor");
        }

        public async Task<List<UserGetDto>> GetAdmins()
        {
            var users = await _userManager.Users.Select(c => new UserGetDto()
            {
                Username = c.UserName,
                Email = c.Email,
                Role = string.Join(",", _userManager.GetRolesAsync(c).Result.ToArray())
            }).ToListAsync();

            return users;
        }
    }
}
