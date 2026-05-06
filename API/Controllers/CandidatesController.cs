using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService) => _candidateService = candidateService;

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _candidateService.GetProfileByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("me")]
        [Authorize(Roles = "Candidate")]
        public async Task<IActionResult> GetMyProfile()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _candidateService.GetProfileByIdAsync(id);
            return Ok(result) ;
        }
    }
}
