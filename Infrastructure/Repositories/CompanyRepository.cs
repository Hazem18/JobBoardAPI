using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly JobBoardContext _jobBoardContext;

        public CompanyRepository(JobBoardContext jobBoardContext)
        {
            _jobBoardContext = jobBoardContext;
        }

        public async Task<Company?> GetByIdAsync(string id)
        => await _jobBoardContext.Companies.FirstOrDefaultAsync(j=>j.Id == id);

        public async Task UpdateAsync(Company company)
        => await _jobBoardContext.SaveChangesAsync();
    }
}
