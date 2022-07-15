using Assessment.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Core.Services
{
    public interface IAuthService
    {
        Task<CustomResponseDto<TokenDto>> Login(LoginDto loginDto);

    }
}
