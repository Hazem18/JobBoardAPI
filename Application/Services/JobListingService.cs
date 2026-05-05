using Application.DTOs.JobListingDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services
{
    public class JobListingService : IJobListingService
    {
        private readonly IJobListingRepository _jobListingRepository;
        private readonly IMapper _mapper;
        

        public JobListingService(IJobListingRepository jobListingRepository, IMapper mapper
            , ICompanyRepository companyRepository)
        {
            _jobListingRepository = jobListingRepository;
            _mapper = mapper;
        }
        public async Task CloseAsync(int id, string companyId)
        {
            var jobListing = await _jobListingRepository.GetByIdAsync(id);
            if (jobListing is null)
                throw NotFoundException.For<JobListing>(id.ToString());
            if (jobListing.CompanyId != companyId)
                throw UnauthorizedException.ForInvalidCredentials();

            jobListing.Status = JobStatus.Closed;
           await _jobListingRepository.UpdateAsync(jobListing);
        }

        public async Task<JobListingResponseDto> CreateAsync(CreateJobListingDto dto, string companyId)
        {
            var jobListing = _mapper.Map<JobListing>(dto);
            jobListing.CompanyId = companyId;
            jobListing.PostedAt = DateTime.UtcNow;
            jobListing.Status = JobStatus.Open;

            await _jobListingRepository.AddAsync(jobListing);

            var fullListing = await _jobListingRepository.GetByIdAsync(jobListing.Id);
            return _mapper.Map<JobListingResponseDto>(fullListing);
        }

        public async Task DeleteAsync(int id, string companyId)
        {
            var jobListing = await _jobListingRepository.GetByIdAsync(id);
            if (jobListing is null)
                throw NotFoundException.For<JobListing>(id.ToString());
            if (jobListing.CompanyId != companyId)
                throw UnauthorizedException.ForInvalidCredentials();

            await _jobListingRepository.DeleteAsync(jobListing);

        }

        public async Task<JobListingResponseDto> GetByIdAsync(int id)
        {
            var jobListing = await _jobListingRepository.GetByIdAsync(id);
            if (jobListing is null)
                throw NotFoundException.For<JobListing>(id.ToString());

            return _mapper.Map<JobListingResponseDto>(jobListing);
        }

        public async Task<List<JobListingResponseDto>> GetByCompanyIdAsync(string companyId)
        {
            var jobListings = await _jobListingRepository.GetByCompanyIdAsync(companyId);
            if (jobListings == null || !jobListings.Any())
                throw NotFoundException.For<JobListing>(companyId);

            return _mapper.Map<List<JobListingResponseDto>>(jobListings);
        }

        public async Task<List<JobListingResponseDto>> GetFilteredAsync(
            string? location, 
            string? jobType, decimal? minSalary,
            decimal? maxSalary,
            string? keyword)
        {
            var joblistingsFiltered = await _jobListingRepository.
                GetFilteredAsync(location,
                jobType,minSalary,
                maxSalary,keyword);

            return _mapper.Map<List<JobListingResponseDto>>(joblistingsFiltered);
        }

        public async Task UpdateAsync(int id, UpdateJobListingDto dto, string companyId)
        {
            var jobListing = await _jobListingRepository.GetByIdAsync(id);
            if (jobListing is null)
                throw NotFoundException.For<JobListing>(id.ToString());
            if (jobListing.CompanyId != companyId)
                throw UnauthorizedException.ForInvalidCredentials();

            _mapper.Map(dto, jobListing);
            await _jobListingRepository.UpdateAsync(jobListing);

        }
    }
}
