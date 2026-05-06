using Application.DTOs.CandidateDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
namespace Application.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepo;
        private readonly IMapper _mapper;

        public CandidateService(ICandidateRepository candidateRepo, IMapper mapper)
        {
            _candidateRepo = candidateRepo;
            _mapper = mapper;
        }

        public async Task<CandidateProfileDto?> GetProfileByIdAsync(string id)
        {
            var candidate = await _candidateRepo.GetByIdAsync(id);
            if (candidate is null)
                throw NotFoundException.For<Candidate>(id);
            return candidate == null ? null : _mapper.Map<CandidateProfileDto>(candidate);
        }
    }
}
