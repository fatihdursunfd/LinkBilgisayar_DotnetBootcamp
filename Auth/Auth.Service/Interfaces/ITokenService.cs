using Auth.Service.Configurations;
using Auth.Service.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Service.Interfaces
{
    public interface ITokenService
    {
        TokenDto CreateToken(IdentityUser user);

        ClientTokenDto CreateTokenByClient(Client client);
    }
}
