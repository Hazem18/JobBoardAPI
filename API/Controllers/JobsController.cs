using Application.DTOs.JobListingDtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobsController : ControllerBase
    {
        private readonly IJobListingService _jobListingService;

        public JobsController(IJobListingService jobListingService)
        {
            _jobListingService = jobListingService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetFiltered(
           [FromQuery] string? location,
            [FromQuery] string? jobType, [FromQuery] decimal? minSalary,
           [FromQuery] decimal? maxSalary,
            [FromQuery] string? keyword)
        {
            var joblistings = await _jobListingService.GetFilteredAsync
                (location,
                jobType,minSalary,
                maxSalary,
                keyword);

            return Ok(joblistings);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var jobListing = await _jobListingService.GetByIdAsync(id);
            return Ok(jobListing);
        }
        
        [HttpPost]
        [Authorize(Roles ="Company")]
        public async Task<IActionResult> Create(CreateJobListingDto dto)
        {
            var companyId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var joblisting =await _jobListingService.CreateAsync(dto, companyId);
            return CreatedAtAction(nameof(GetById), new {id = joblisting.Id} ,joblisting);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> Update(int id,UpdateJobListingDto dto)
        {
            var companyId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _jobListingService.UpdateAsync(id, dto, companyId);
            return NoContent(); 
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> Delete(int id)
        {
            var companyId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _jobListingService.DeleteAsync(id, companyId);
            return NoContent();
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> Close(int id)
        {

            var companyId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _jobListingService.CloseAsync(id, companyId);
            return NoContent();

        }

        [HttpGet("my-jobs")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> GetMyJobs()
        {
            var companyId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var jobs = await _jobListingService.GetByCompanyIdAsync(companyId);
            return Ok(jobs);
        }
    }
}
