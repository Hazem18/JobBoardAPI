using Domain.Entities;
namespace Domain.Interfaces
{
    public interface ICompanyRepository
    {
        Task<Company?> GetByIdAsync(string id);
        Task UpdateAsync(Company company);
    }
}
