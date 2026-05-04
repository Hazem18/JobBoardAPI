using Application.DTOs.JobListingDtos;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;


namespace Application.Mappings
{
    public class JobListingProfile : Profile
    {
        public JobListingProfile()
        {
            CreateMap<JobListing, JobListingResponseDto>().
                ForMember(dest => dest.CompanyName, ops => ops.MapFrom(src => src.Company.CompanyName))
                .ForMember(dest=>dest.ApplicationCount, ops=>ops.MapFrom(src=>src.JobApplications.Count()))
                .ForMember(dest=>dest.JobType, ops => ops.MapFrom(src=>src.JobType.ToString()))
                .ForMember(dest=>dest.Status,ops=>ops.MapFrom(src=>src.Status.ToString()));
            CreateMap<CreateJobListingDto, JobListing>()
                .ForMember(dest => dest.JobType,
                    ops => ops.MapFrom(src => Enum.Parse<JobType>(src.JobType)));

            CreateMap<UpdateJobListingDto, JobListing>()
                .ForMember(dest => dest.JobType,
                    ops => ops.MapFrom(src => Enum.Parse<JobType>(src.JobType)));
            
        }
    }
}
