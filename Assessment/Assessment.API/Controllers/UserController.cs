using Assessment.Core.DTOs;
using Assessment.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.API.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(UserDto userDto) 
        {
            await userService.CreateUser(userDto);
            return Ok();
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            await userService.AddRole(roleName);
            return Ok();
        }

        [HttpPost("MakeAdmin")]
        public async Task<IActionResult> MakeAdmin(string email)
        {
            await userService.MakeAdmin(email);
            return Ok();
        }
    }
}
