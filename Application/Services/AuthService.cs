using Application.DTOs.Auth;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using System.Data;


namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        public AuthService(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager
            ,ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
          
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var  user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is null)
                throw NotFoundException.ForUser<ApplicationUser>(dto.Email);
            if (!await _userManager.CheckPasswordAsync(user,dto.Password))
                throw UnauthorizedException.ForInvalidCredentials();

            var roles =await  _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "Unknown";
            var token =  _tokenService.GenerateToken(user, role);

            return new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                FullName = user.FullName,
                Role = role
            };

        }

        public async Task<AuthResponseDto> RegisterCandidateAsync(RegisterCandidateDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is not null)
                throw DuplicateEmailException.For<ApplicationUser>(dto.Email);

            var candidate = new Candidate
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName,
                Skills = dto.Skills ?? "",
                Bio = dto.Bio ?? "",
                ResumeUrl = dto.ResumeUrl ?? ""
            };
            var result = await _userManager.CreateAsync(candidate, dto.Password);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            await _userManager.AddToRoleAsync(candidate, "Candidate");
            var token =  _tokenService.GenerateToken(candidate, "Candidate");
            return new AuthResponseDto
            {
                Token = token,
                Email = candidate.Email,
                FullName = candidate.FullName,
                Role = "Candidate"
            };

        }

        public async Task<AuthResponseDto> RegisterCompanyAsync(RegisterCompanyDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is not null)
                throw DuplicateEmailException.For<ApplicationUser>(dto.Email);

            var company = new Company
            {
                UserName = dto.Email,
                Email = dto.Email,
                CompanyName = dto.CompanyName,
                FullName = dto.FullName,
                Website = dto.Website,
                Description = dto.Description
               
            };

            var result = await _userManager.CreateAsync(company, dto.Password);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            await _userManager.AddToRoleAsync(company, "Company");
            var token = _tokenService.GenerateToken(company, "Company");

            return new AuthResponseDto
            {
                Token = token,
                Email = company.Email,
                FullName = company.FullName,
                Role = "Company"
            };
        }
    }
}
