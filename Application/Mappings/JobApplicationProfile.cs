using Application.DTOs.JobApplicationDtos;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.Mappings
{
    public class JobApplicationProfile : Profile
    {
        public JobApplicationProfile()
        {
            CreateMap<JobApplication, JobApplicationResponseDto>()
            .ForMember(dest => dest.JobTitle,
               opt => opt.MapFrom(src => src.JobListing.Title))
                .ForMember(dest => dest.CompanyName,
                 opt => opt.MapFrom(src => src.JobListing.Company.CompanyName))
                .ForMember(dest => dest.CandidateName,
                 opt => opt.MapFrom(src => src.Candidate.FullName))
                  .ForMember(dest => dest.Status,
                 opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<CreateJobApplicationDto, JobApplication>();
            CreateMap<UpdateApplicationStatusDto, JobApplication>()
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => Enum.Parse<ApplicationStatus>(src.Status)));
        }
    }
}
