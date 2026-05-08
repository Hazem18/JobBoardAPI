using Application.DTOs.AIDtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AIController : ControllerBase
    {
        private readonly IAIService _aiService;

        public AIController(IAIService aiService)
        {
            _aiService = aiService;
        }

        [HttpPost("cover-letter")]
        [Authorize(Roles = "Candidate")]
        public async Task<IActionResult> GenerateCoverLetter(CoverLetterRequestDto dto)
        {
            var result = await _aiService.GenerateCoverLetterAsync(dto);
            return Ok(result);
        }

        [HttpPost("match-score")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> GenerateMatchScore(MatchScoreRequestDto dto)
        {
            var result = await _aiService.GenerateMatchScoreAsync(dto);
            return Ok(result);
        }
    }
}
