using AutoMapper;
using ShippingSystem.DTOs.Authentication;
using ShippingSystem.DTOS.City;
using ShippingSystem.DTOS.Governate;
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


            CreateMap<Governate, GovernateDto>().ReverseMap();

            //CreateMap<City, CityDto>()
            //.ForMember(dest => dest.Governate_Id, opt => opt.MapFrom(src => src.Governate.Id));

            CreateMap<CityDto, City>();
           //.ForMember(dest => dest.Governate.Id, opt => opt.MapFrom(src => src.Governate_Id));

            CreateMap<City,CityDto>()
            .ForMember(dest => dest.Governate_Id, opt => opt.MapFrom(src => src.Governate.Id));







        }
    }
}
