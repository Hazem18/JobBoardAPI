using Application.DTOs.Auth;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        => Ok(await _authService.LoginAsync(dto));
        [HttpPost("RegisterCompany")]
        public async Task<IActionResult> RegisterCompany(RegisterCompanyDto dto)
            => Ok(await _authService.RegisterCompanyAsync(dto));

        [HttpPost("RegisterCandidate")]
        public async Task<IActionResult> RegisterCandidte(RegisterCandidateDto dto)
            => Ok(await _authService.RegisterCandidateAsync(dto));
    }
}
