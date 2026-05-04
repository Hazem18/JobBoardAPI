using Application.DTOs.JobApplicationDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;


namespace Application.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly IMapper _mapper;
        private readonly IJobListingRepository _jobListingRepository;

        public JobApplicationService(IJobApplicationRepository jobApplicationRepository, IMapper mapper, 
            IJobListingRepository jobListingRepository)
        {
            _jobApplicationRepository = jobApplicationRepository;
            _mapper = mapper;
            _jobListingRepository = jobListingRepository;
        }

        public async Task<JobApplicationResponseDto> ApplyAsync(int jobListingId, string candidateId, CreateJobApplicationDto dto)
        {
            var joblisting = await _jobListingRepository.GetByIdAsync(jobListingId);
            if (joblisting is null || joblisting.Status == JobStatus.Closed)
                throw NotFoundException.For<JobListing>(jobListingId.ToString());
            if (await _jobApplicationRepository.HasCandidateAppliedAsync(candidateId, jobListingId))
                throw DuplicateApplicationException.For<JobListing>(jobListingId);
            var jobapp = _mapper.Map<JobApplication>(dto);
            jobapp.AppliedAt = DateTime.UtcNow;
            jobapp.CandidateId = candidateId;
            jobapp.JobListingId = jobListingId;
            await _jobApplicationRepository.AddAsync(jobapp);
            var fullApp = await _jobApplicationRepository.GetByIdAsync(jobapp.Id);
            return _mapper.Map<JobApplicationResponseDto>(fullApp);
        }

        public async Task<List<JobApplicationResponseDto>> GetByJobListingIdAsync(int jobListingId, string companyId)
        {
            var joblisting = await _jobListingRepository.GetByIdAsync(jobListingId);
            if (joblisting is null)
                throw NotFoundException.For<JobListing>(jobListingId.ToString());
            if (!(joblisting.CompanyId == companyId))
                throw UnauthorizedException.ForInvalidCredentials();

            var jobapps = await _jobApplicationRepository.GetByJobListingIdAsync(jobListingId);

            return _mapper.Map<List<JobApplicationResponseDto>>(jobapps);
        }

        public async Task<List<JobApplicationResponseDto>> GetMyApplicationsAsync(string candidateId)
        {
            var jobapps = await _jobApplicationRepository.GetByCandidateIdAsync(candidateId);

            return _mapper.Map<List<JobApplicationResponseDto>>(jobapps);
        }

        public async Task UpdateStatusAsync(int applicationId, string companyId, UpdateApplicationStatusDto dto)
        {
            var jobapp =await _jobApplicationRepository.GetByIdAsync(applicationId);
            if (jobapp is null)
                throw NotFoundException.For<JobApplication>(applicationId.ToString());
            var joblisting = await _jobListingRepository.GetByIdAsync(jobapp.JobListingId);
            if (!(joblisting.CompanyId == companyId))
                throw UnauthorizedException.ForInvalidCredentials();

            _mapper.Map(dto, jobapp);
            await _jobApplicationRepository.UpdateAsync(jobapp);
        }
    }
}
