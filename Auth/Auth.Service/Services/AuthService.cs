using Auth.Data.Interfaces;
using Auth.Data.Models;
using Auth.Service.Dtos;
using Auth.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepo<RefreshToken> _refreshTokenService;

        public AuthService(IUnitOfWork unitOfWork, 
                           UserManager<IdentityUser> userManager, 
                           ITokenService tokenService, 
                           IGenericRepo<RefreshToken> refreshTokenService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _tokenService = tokenService;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null) 
                throw new ArgumentNullException(nameof(loginDto));

            var user = _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return Response<TokenDto>.Fail("Email or Password is wrong", 400, true);

            if (! await _userManager.CheckPasswordAsync(await user , loginDto.Password))
                return Response<TokenDto>.Fail("Email or Password is wrong", 400, true);


            var token = _tokenService.CreateToken(await user);

            var userId = (await user).Id;
            var userRefreshToken = await _refreshTokenService.Where( x => x.UserId == userId).SingleOrDefaultAsync();

            if (userRefreshToken is null)
            {
                await _refreshTokenService.AddAsync(new RefreshToken()
                {
                    UserId = userId,
                    UserRefreshToken = token.RefreshToken,
                    Expiration = token.RefreshTokenExpiration
                }) ;
            }
            else
            {
                userRefreshToken.UserRefreshToken = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }

            await _unitOfWork.CommmitAsync();

            return Response<TokenDto>.Success(token, 200);
        }

        public Response<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _refreshTokenService.Where(x => x.UserRefreshToken == refreshToken).SingleOrDefaultAsync();

            if (existRefreshToken == null)
                return Response<TokenDto>.Fail("Refresh token not found", 404, true);

            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);

            if (user == null)
                return Response<TokenDto>.Fail("User Id not found", 404, true);

            var tokenDto = _tokenService.CreateToken(user);

            existRefreshToken.UserRefreshToken = tokenDto.RefreshToken;
            existRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;

            await _unitOfWork.CommmitAsync();

            return Response<TokenDto>.Success(tokenDto, 200);

        }

        public async Task<Response<NoDataDto>> RemoveRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _refreshTokenService.Where(x => x.UserRefreshToken == refreshToken).SingleOrDefaultAsync();

            if (existRefreshToken == null)
                return Response<NoDataDto>.Fail("Refresh token not found", 404, true);

            _refreshTokenService.Remove(existRefreshToken);

            await _unitOfWork.CommmitAsync();

            return Response<NoDataDto>.Success(200);
        }
    }
}
