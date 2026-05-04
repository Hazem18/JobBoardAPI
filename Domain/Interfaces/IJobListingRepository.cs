using Domain.Entities;
namespace Domain.Interfaces
{
    public interface IJobListingRepository
    {
        Task<List<JobListing>> GetAllAsync();
        Task<JobListing?> GetByIdAsync(int id);
        Task AddAsync(JobListing jobListing);
        Task UpdateAsync(JobListing jobListing);
        Task DeleteAsync(JobListing jobListing);
        Task<bool> ExistsAsync(int id);
        Task<List<JobListing>> GetFilteredAsync(string? location, string? jobType, 
            decimal? minSalary, decimal? maxSalary, 
            string? keyword);
        Task<List<JobListing>> GetByCompanyIdAsync(string companyId);
    }
}
