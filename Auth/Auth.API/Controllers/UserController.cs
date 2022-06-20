using Auth.Data.Model;
using Auth.Service.Dtos;
using Auth.Service.Interfaces;
using Auth.Service.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        private readonly IAuthService authService;

        public UserController( IUserService userService, IAuthService authService)
        {
            this.userService = userService;
            this.authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await userService.GetUsers();
            return Ok(users);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(LoginDto user)
        {
            var response = await authService.AuthenticateAsync(user);
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh(Token token)
        {
            var response = await authService.Refresh(token);
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateUser(UserDto user)
        {
            var response = await userService.CreateUserAsync(user);
            return Ok(response);
        }

    }
}
