using Auth.Service.Dtos;
using Auth.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> GetById()
        {
            var id = HttpContext.User.Identity.IsAuthenticated;

            return Ok();

            //var result = await userService.GetUserByNameAsync(HttpContext.User.Identity.Name);
            //return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto createUserDto)
        {
            var result = await userService.CreateUserAsync(createUserDto);
            return Ok(result);
        }


    }
}
