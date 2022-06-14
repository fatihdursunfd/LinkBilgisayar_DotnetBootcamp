using API.Dto;
using API.Model;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userService.GetAllUsers();
            if (users is null)
                return NotFound("User not found");

            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserByID(int id)
        {
            if (id < 0)
                return BadRequest("id must be greater than 0");

            var user = _userService.GetUserByID(id);
            if (user is null)
                return NotFound("User not found");

            return Ok(user);
        }

        [HttpPost]
        public IActionResult AddUser(UserPostDto user)
        {
            if (!ModelState.IsValid)
                return BadRequest();    

            _userService.AddUser(user);

            return Ok("User added succesfully");
        }

        [HttpDelete]
        public IActionResult RemoveUser(int id)
        {
            if (id < 0)
                return BadRequest("id must be greater than 0");
            var result = _userService.RemoveUser(id);

            if(result)
                return Ok("User deleted succesfully");
            else
                return NotFound("User not found");

        }

        [HttpPut]
        public IActionResult UpdateUser(int id , UserUpdateDto user)
        {
            if (id < 0)
                return BadRequest("id must be greater than 0");

            if (!ModelState.IsValid)
                return BadRequest();

            var result = _userService.UpdateUser(id ,user);

            if (result)
                return Ok("User updated succesfully");
            else
                return NotFound("User not found");

        }
    }
}
