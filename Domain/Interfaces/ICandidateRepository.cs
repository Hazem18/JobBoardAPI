using Domain.Entities;
namespace Domain.Interfaces
{
    public interface ICandidateRepository
    {
        Task<Candidate?> GetByIdAsync(string id);
        Task UpdateAsync(Candidate candidate);
    }
}
