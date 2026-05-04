using Application.DTOs.JobApplicationDtos;


namespace Application.Interfaces
{
    public interface IJobApplicationService
    {
        Task<JobApplicationResponseDto> ApplyAsync(int jobListingId, string candidateId, CreateJobApplicationDto dto);
        Task<List<JobApplicationResponseDto>> GetMyApplicationsAsync(string candidateId);
        Task<List<JobApplicationResponseDto>> GetByJobListingIdAsync(int jobListingId, string companyId);
        Task UpdateStatusAsync(int applicationId, string companyId, UpdateApplicationStatusDto dto);
    }
}
