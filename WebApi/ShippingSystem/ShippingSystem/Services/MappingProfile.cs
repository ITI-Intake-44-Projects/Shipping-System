using AutoMapper;
using ShippingSystem.DTOs.Authentication;
using ShippingSystem.DTOs.Groups;
using ShippingSystem.DTOs.Privileges;
using ShippingSystem.Models;

namespace ShippingSystem.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDetailsDTO>().ReverseMap();

            CreateMap<ApplicationUser, RegisterDTO>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ReverseMap();

            CreateMap<Group, GroupDTO>()
                .ForMember(dest => dest.GroupPrivileges, opt => opt.MapFrom(src => src.Privileges))
                .ReverseMap()
                .ForMember(dest => dest.Privileges, opt => opt.MapFrom(src => src.GroupPrivileges));

            CreateMap<GroupResponseDTO, GroupDTO>()
                .ForMember(dest => dest.Name, opt=>opt.MapFrom(src=>src.Name))
                .ForMember(dest => dest.GroupPrivileges, opt=>opt.MapFrom(src=>src.GroupPrivileges))
                .ReverseMap();

            CreateMap<GroupResponseDTO, Group>()
                .ForMember(dest => dest.Name, opt=>opt.MapFrom(src=>src.Name))
                .ForMember(dest => dest.DateAdded, opt=>opt.MapFrom(src=>src.DateAdded))
                .ForMember(dest => dest.Privileges, opt=>opt.MapFrom(src=>src.GroupPrivileges))
                .ReverseMap();

            CreateMap<Privilege, PrivilegeDTO>().ReverseMap();

            CreateMap<Privilege, PrivilegeResponseDTO>().ReverseMap();

            CreateMap<GroupPrivilegeDTO, GroupPrivilege>()
                .ForMember(dest => dest.Privelege_Id, opt => opt.MapFrom(src => src.Privelege_Id))
                .ReverseMap();
        }
    }
}