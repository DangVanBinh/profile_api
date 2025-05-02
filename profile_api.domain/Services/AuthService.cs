using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using profile_api.domain.DTOs.Auth;
using profile_api.domain.Entities.User;
using profile_api.domain.Services.Interfaces;
using Microsoft.Extensions.Configuration;


namespace profile_api.domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _config;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IMapper _mapper;

        public AuthService(
            UserManager<AppUser> userManager, 
            IConfiguration config, 
            TokenValidationParameters tokenValidationParameters,
            IMapper mapper)
        {
            _userManager = userManager;
            _config = config;
            _tokenValidationParameters = tokenValidationParameters;
            _mapper = mapper;
        }

        public async Task<AuthResult?> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if(user == null) 
                throw new Exception($"Tài khoản {request.UserName} không tồn tại!");
            var checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!checkPassword)
                throw new Exception("Sai mật khẩu");
            return await GenerateAuthResult(user);
        }

        public Task LogoutAsync(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthResult?> RefreshTokenAsync(string refreshToken)
        {
            var principal = GetPrincipalFromToken(refreshToken);
            if (principal == null) return null;

            var user = await _userManager.FindByNameAsync(principal.Identity.Name);
            if (user == null) return null;

            return await GenerateAuthResult(user);
        }

        public async Task<AuthResult?> RegisterAsync(RegisterRequest request)
        {
            var existingUserByUsername = await _userManager.FindByNameAsync(request.UserName);
            if (existingUserByUsername != null)
                throw new Exception("Username already exists.");

            var existingUserByEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingUserByEmail != null)
                throw new Exception("Email already exists.");

            var hasher = new PasswordHasher<AppUser>();
            var user = new AppUser
            {
                UserName = request.UserName,
                FullName = request.FullName,
                Email = request.Email,
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                throw new Exception("Đăng ký thất bại.");

            return await GenerateAuthResult(user);
        }

        private async Task<AuthResult> GenerateAuthResult(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);


            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.UserName!),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Bạn có thể tạo refresh token ngẫu nhiên & lưu vào DB
            var refreshToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            return new AuthResult
            {
                AccessToken = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken,
                ExpireAt = tokenDescriptor.Expires!.Value
            };
        }

        private ClaimsPrincipal? GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                if (validatedToken is JwtSecurityToken jwt
                    && jwt.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return principal;
                }
            }
            catch { }
            return null;
        }
    }
}
