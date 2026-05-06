using Application.DTOs.CompanyDtos;
using AutoMapper;
using Domain.Entities;
namespace Application.Mappings
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyProfileDto>()
            .ForMember(dest => dest.JobCount, opt => opt.Ignore())
            .ForMember(dest => dest.OpenJobCount, opt => opt.Ignore());
        }
    }
}
