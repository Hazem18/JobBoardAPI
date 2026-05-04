using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly JobBoardContext _jobBoardContext;

        public JobApplicationRepository(JobBoardContext jobBoardContext)
        {
            _jobBoardContext = jobBoardContext;
        }

        public async Task AddAsync(JobApplication jobApplication)
        {
            _jobBoardContext.JobApplications.Add(jobApplication);
           await _jobBoardContext.SaveChangesAsync();

        }

        public async Task DeleteAsync(JobApplication jobApplication)
        {
            _jobBoardContext.JobApplications.Remove(jobApplication);
            await _jobBoardContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        => await _jobBoardContext.JobApplications.AnyAsync(j=>j.Id == id);

        public async Task<List<JobApplication>> GetAllAsync()
       => await _jobBoardContext.JobApplications
            .Include(j => j.Candidate)
            .Include(j => j.JobListing)
            .ToListAsync();

        public async Task<List<JobApplication>> GetByCandidateIdAsync(string candidateId)
        => await _jobBoardContext.JobApplications
            .Include(j => j.Candidate)
            .Include(j => j.JobListing)
            .Where(j => j.CandidateId == candidateId)
            .ToListAsync();
            

        public async Task<JobApplication?> GetByIdAsync(int id)
        => await _jobBoardContext.JobApplications
            .Include(j => j.Candidate)
            .Include(j => j.JobListing)
            .FirstOrDefaultAsync(j=>j.Id == id);
        public async Task<List<JobApplication>> GetByJobListingIdAsync(int jobListingId)
        => await _jobBoardContext.JobApplications
            .Include(j => j.Candidate)
            .Include(j => j.JobListing)
            .Where(j => j.JobListingId == jobListingId)
            .ToListAsync();

        public async Task<bool> HasCandidateAppliedAsync(string candidateId, int jobListingId)
        => await _jobBoardContext.JobApplications.AnyAsync(j=>j.CandidateId == candidateId && j.JobListingId == jobListingId);

        public async Task UpdateAsync(JobApplication jobApplication)
        => await _jobBoardContext.SaveChangesAsync();
    }
}
