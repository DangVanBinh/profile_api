using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using profile_api.domain.DTOs.Auth;

namespace profile_api.domain.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult?> RegisterAsync(RegisterRequest request);
        Task<AuthResult?> LoginAsync(LoginRequest request);
        Task<AuthResult?> RefreshTokenAsync(string refreshToken);
        Task LogoutAsync(string token);
    }
}
