using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService) => _companyService = companyService;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _companyService.GetProfileByIdAsync(id);
            return result != null ? Ok(result) : NotFound($"Company {id} not found");
        }

        [HttpGet("me")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> GetMyProfile()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _companyService.GetProfileByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }
    }
}
