using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly JobBoardContext _jobBoardContext;

        public CandidateRepository(JobBoardContext jobBoardContext)
        {
            _jobBoardContext = jobBoardContext;
        }

        public async Task<Candidate?> GetByIdAsync(string id)
        => await _jobBoardContext.Candidates.FirstOrDefaultAsync(j=>j.Id == id);

        public async Task UpdateAsync(Candidate candidate)
        => await _jobBoardContext.SaveChangesAsync();
    }
}
