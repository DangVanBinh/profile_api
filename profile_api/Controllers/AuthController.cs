using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using profile_api.domain.DTOs.Auth;
using profile_api.domain.Services.Interfaces;

namespace profile_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        public AuthController(IMapper mapper, ILogger<AuthController> logger, IAuthService authService)
        {
            _mapper = mapper;
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var result = await _authService.RegisterAsync(request);
                return Ok(result);
            } 
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var result = await _authService.LoginAsync(request);
                if (result == null)
                    return Unauthorized();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] string refreshToken)
        {
            var result = await _authService.RefreshTokenAsync(refreshToken);
            if (result == null)
                return Unauthorized("Token không hợp lệ.");
            return Ok(result);
        }

    }
}
