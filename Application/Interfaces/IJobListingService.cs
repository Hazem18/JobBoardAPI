using Application.DTOs.JobListingDtos;


namespace Application.Interfaces
{
    public interface IJobListingService
    {
        Task<List<JobListingResponseDto>> GetFilteredAsync(string? location, string? jobType, decimal? minSalary, decimal? maxSalary, string? keyword);
        Task<JobListingResponseDto> GetByIdAsync(int id);
        Task<JobListingResponseDto> CreateAsync(CreateJobListingDto dto, string companyId);
        Task UpdateAsync(int id, UpdateJobListingDto dto, string companyId);
        Task DeleteAsync(int id, string companyId);
        Task CloseAsync(int id, string companyId);
    }
}
