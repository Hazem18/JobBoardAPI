using Application.DTOs.CompanyDtos;
namespace Application.Interfaces
{
    public interface ICompanyService
    {
        Task<CompanyProfileDto?> GetProfileByIdAsync(string id);
    }
}
