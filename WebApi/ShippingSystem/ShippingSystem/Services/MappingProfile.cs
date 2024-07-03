using AutoMapper;
using ShippingSystem.DTOs.Authentication;
using ShippingSystem.DTOs.Representatives;
using ShippingSystem.Models;

namespace ShippingSystem.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDetailsDTO>().ReverseMap();
            
            //CreateMap<ApplicationUser, RegisterDTO>().ReverseMap();
            CreateMap<ApplicationUser, RegisterDTO>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ReverseMap();

            CreateMap<Representative, RepresentativeDTO>()
            .ForMember(dest => dest.GovernateIds, opt => opt.MapFrom(src => src.RepresentativeGovernates.Select(g => g.Governate_Id).ToList()));

            CreateMap<RepresentativeDTO, Representative>()
                .ForMember(dest => dest.RepresentativeGovernates, opt => opt.MapFrom(src => src.GovernateIds.Select(id => new RepresentativeGovernate { Governate_Id = id }).ToList()));
        }
    }
}
