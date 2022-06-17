using Auth.Service.Dtos;
using Auth.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken(LoginDto loginDto)
        {
            var result = await _authService.CreateTokenAsync(loginDto);

            return Ok(result);
        }

        [HttpPost("createtokenbyrefreshtoken")]
        public async Task<IActionResult> CreateTokenByRefreshToken(string refreshToken)
        {
            var result = await _authService.CreateTokenByRefreshToken(refreshToken);
            return Ok(result);
        }

        
    
    }
}
