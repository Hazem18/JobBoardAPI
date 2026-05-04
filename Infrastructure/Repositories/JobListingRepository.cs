using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories
{
    public class JobListingRepository : IJobListingRepository
    {
        private readonly JobBoardContext _jobBoardContext;

        public JobListingRepository(JobBoardContext jobBoardContext)
        {
            _jobBoardContext = jobBoardContext;
        }

        public async Task AddAsync(JobListing jobListing)
        {
            _jobBoardContext.JobListings.Add(jobListing);
            await _jobBoardContext.SaveChangesAsync();

        }

        public async Task DeleteAsync(JobListing jobListing)
        {
            _jobBoardContext.JobListings.Remove(jobListing);
            await _jobBoardContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
         => await _jobBoardContext.JobListings.AnyAsync(j => j.Id == id);

        public async Task<List<JobListing>> GetAllAsync()
        => await _jobBoardContext.JobListings.
            Include(j => j.Company)
            .Include(j=> j.JobApplications)
            .ToListAsync();
            

        public async Task<List<JobListing>> GetByCompanyIdAsync(string companyId)
         => await _jobBoardContext.JobListings.
            Include(j => j.Company).
            Include(j => j.JobApplications)
            .Where(j=>j.CompanyId == companyId)
            .ToListAsync();

        public async Task<JobListing?> GetByIdAsync(int id)
             => await _jobBoardContext.JobListings.
            Include(j => j.Company).
            Include(j => j.JobApplications)
            .FirstOrDefaultAsync(j => j.Id == id);

        public async Task<List<JobListing>> GetFilteredAsync
            (string? location, 
            string? jobType,
            decimal? minSalary,
            decimal? maxSalary, 
            string? keyword)
        {
            var query = _jobBoardContext.JobListings
                .Include(j => j.Company)
                .Include(j=>j.JobApplications)
                .Where(j => j.Status == JobStatus.Open)
                .AsQueryable();
            if (!string.IsNullOrEmpty(location))
                 query = query.Where(q => q.Location.Contains(location));
            if (!string.IsNullOrEmpty(jobType) && Enum.TryParse<JobType>(jobType, out var type))
                query = query.Where(q=>q.JobType == type);

            if (minSalary.HasValue)
                query = query.Where(j => j.SalaryMin >= minSalary.Value);
            
            if (maxSalary.HasValue)
                query = query.Where(j => j.SalaryMax <= maxSalary.Value);

            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(j => j.Title.Contains(keyword) || j.Description.Contains(keyword));



            return await query.ToListAsync();
        }

        public async  Task UpdateAsync(JobListing jobListing)
        => await _jobBoardContext.SaveChangesAsync();
    }
}
