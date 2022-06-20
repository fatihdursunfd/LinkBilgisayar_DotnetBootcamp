using Auth.Data.Model;
using Auth.Service.Dtos;
using Auth.Service.Interfaces;
using Auth.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IJwtService jwtRepo;
        private readonly IUserService userService;

        public AuthService(IJwtService jwtRepo, IUserService userService)
        {
            this.jwtRepo = jwtRepo;
            this.userService = userService;
        }

        public async Task<Response<Token>> AuthenticateAsync(LoginDto user)
        {
            var validUser = await userService.IsValidUserAsync(user);

            if (!validUser)
                return new Response<Token>() { Error = "Incorrect username or password!", Data = null, StatusCode = 401 };
                    

            var token = jwtRepo.GenerateToken(user.Name);

            if (token == null)
                return new Response<Token>() { Error = "Invalid Attempt!", Data = null, StatusCode = 401 };

            UserRefreshToken obj = new UserRefreshToken
            {
                RefreshToken = token.Refresh_Token,
                UserName = user.Name
            };

            await userService.AddUserRefreshTokens(obj);
            userService.SaveCommit();

            return new Response<Token>() { Data = token, Error = "", StatusCode = 200 };

        }

        public async Task<Response<Token>> Refresh(Token token)
        {
            var principal = jwtRepo.GetPrincipalFromExpiredToken(token.Access_Token);
            var username = principal.Identity?.Name;

            //retrieve the saved refresh token from database
            var savedRefreshToken = userService.GetSavedRefreshTokens(username, token.Refresh_Token);

            if (savedRefreshToken.RefreshToken != token.Refresh_Token)
                return new Response<Token>() { Error = "Invalid attempt!", Data = null, StatusCode = 401 };

            var newJwtToken = jwtRepo.GenerateRefreshToken(username);

            if (newJwtToken == null)
                return new Response<Token>() { Error = "Invalid attempt!", Data = null, StatusCode = 401 };

            // saving refresh token to the db
            UserRefreshToken obj = new UserRefreshToken
            {
                RefreshToken = newJwtToken.Refresh_Token,
                UserName = username
            };

            userService.DeleteUserRefreshTokens(username, token.Refresh_Token);
            await userService.AddUserRefreshTokens(obj);
            userService.SaveCommit();

            return new Response<Token>() { Data = newJwtToken, Error = "", StatusCode = 200 };

        }
    }
}
