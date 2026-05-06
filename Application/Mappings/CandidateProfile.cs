using Application.DTOs.CandidateDtos;
using AutoMapper;
using Domain.Entities;
namespace Application.Mappings
{
    public class CandidateProfile : Profile
    {
        public CandidateProfile()
        {
            CreateMap<Candidate, CandidateProfileDto>();
        }
        
    }
}
