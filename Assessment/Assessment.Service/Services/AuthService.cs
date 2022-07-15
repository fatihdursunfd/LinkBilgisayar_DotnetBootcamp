using Assessment.Core.DTOs;
using Assessment.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<CustomResponseDto<TokenDto>> Login(LoginDto loginDto)
        {
            var user =  await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is null)
                return CustomResponseDto<TokenDto>.Fail(404, "email or password wrong");

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return CustomResponseDto<TokenDto>.Fail(404, "email or password wrong");

            var token = await GenerateToken(user);
            var tokenDto = new TokenDto() { Token = token };

            return CustomResponseDto<TokenDto>.Success(200, tokenDto);
            
        }

        private async Task<string> GenerateToken(IdentityUser user)
        {

            var accessTokenExpiration = DateTime.Now.AddMinutes(30);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysecuritykeymysecuritykeymysecuritykeymysecuritykeymysecuritykey"));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var claims = await GetClaims(user);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: "www.myapi.com",
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: claims,
                signingCredentials: signingCredentials
           );

            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);

            return token;

        }

        private async Task<List<Claim>> GetClaims(IdentityUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Aud, "www.myapi.com"));
            userRoles.ToList().ForEach(x =>
            {
                claims.Add(new Claim(ClaimTypes.Role, x));
            });

            return claims;
        }
    }
}
