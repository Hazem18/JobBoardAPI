using Application.DTOs.JobApplicationDtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IJobApplicationService _jobApplicationService;

        public JobApplicationsController(IJobApplicationService jobApplicationService)
        {
            _jobApplicationService = jobApplicationService;
        }

        [HttpPost("{jobListingId}")]
        [Authorize(Roles = "Candidate")]
        public async Task<IActionResult> Apply(int jobListingId, CreateJobApplicationDto dto)
        {
            var candidateId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var jobApp = await _jobApplicationService.ApplyAsync(jobListingId,candidateId,dto);
            return Created(string.Empty, jobApp);
        }
        [HttpGet("Mine")]
        [Authorize(Roles = "Candidate")]
        public async Task<IActionResult> Mine ()
        {
            var candidateId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var jobApps = await _jobApplicationService.GetMyApplicationsAsync(candidateId);
            return Ok(jobApps);
        }
        [HttpGet("{jobListingId}")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> Applications(int jobListingId)
        {
            var companyId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var jobApps = await _jobApplicationService.GetByJobListingIdAsync(jobListingId, companyId);
            return Ok(jobApps);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateApplicationStatusDto dto)
        {
            var companyId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _jobApplicationService.UpdateStatusAsync(id,companyId,dto);
            return NoContent();
        }
    }
}
