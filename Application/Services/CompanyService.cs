using Application.DTOs.CompanyDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
namespace Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepo;
        private readonly IJobListingRepository _jobRepo;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepo, IJobListingRepository jobRepo, IMapper mapper)
        {
            _companyRepo = companyRepo;
            _jobRepo = jobRepo;
            _mapper = mapper;
        }

        public async Task<CompanyProfileDto?> GetProfileByIdAsync(string id)
        {
            var company = await _companyRepo.GetByIdAsync(id);
            if (company == null)
                throw NotFoundException.For<Company>(id);

  
            var jobs = await _jobRepo.GetByCompanyIdAsync(id);

            var dto = _mapper.Map<CompanyProfileDto>(company);
            dto.JobCount = jobs.Count;
            dto.OpenJobCount = jobs.Count(j => j.Status == JobStatus.Open);

            return dto;
        }
    }
}
