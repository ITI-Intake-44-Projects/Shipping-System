using AutoMapper;
using ShippingSystem.DTOs;
using ShippingSystem.DTOs.Authentication;
//using ShippingSystem.DTOs.Employee;
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
            // Employee mappings
            CreateMap<Employee, EmployeeDTO>()
              .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.Branch_Id))
              .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Name));

            CreateMap<EmployeeDTO, Employee>()
                .ForMember(dest => dest.Branch_Id, opt => opt.MapFrom(src => src.BranchId))
                .ForMember(dest => dest.Branch, opt => opt.Ignore());
        }
    }
}
