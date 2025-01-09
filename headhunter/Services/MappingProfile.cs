using AutoMapper;
using headhunter.Dtos;
using headhunter.Entities;

namespace headhunter.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Resume, ResumeDto>()
                .ForMember(d => d.Email, o => o.MapFrom(s => s.ContactInformation.Email))
                .ForMember(d => d.PhoneNumber, o => o.MapFrom(s => s.ContactInformation.PhoneNumber));

            CreateMap<Company, CompanyDto>();
            CreateMap<Vacancy, VacancyDto>();
            CreateMap<AppUser, AppUserDto>();
        }
    }
}
