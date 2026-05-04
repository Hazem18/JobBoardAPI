using Domain.Entities;
namespace Domain.Interfaces
{
    public interface IJobApplicationRepository
    {
        Task<List<JobApplication>> GetAllAsync();
        Task<JobApplication?> GetByIdAsync(int id);
        Task AddAsync(JobApplication jobApplication);
        Task UpdateAsync(JobApplication jobApplication);
        Task DeleteAsync(JobApplication jobApplication);
        Task<bool> ExistsAsync(int id);
        Task<List<JobApplication>> GetByJobListingIdAsync(int jobListingId);
        Task<List<JobApplication>> GetByCandidateIdAsync(string candidateId);
        Task<bool> HasCandidateAppliedAsync(string candidateId, int jobListingId);
    }
}
