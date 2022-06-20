using Auth.Data.Model;
using Auth.Service.Dtos;
using Auth.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Service.Interfaces
{
    public interface IAuthService
    {
        Task<Response<Token>> AuthenticateAsync(LoginDto user);

        Task<Response<Token>> Refresh(Token token);
    }
}
