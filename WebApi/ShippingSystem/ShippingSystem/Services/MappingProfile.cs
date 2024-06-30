using AutoMapper;
using Microsoft.AspNetCore.Identity;
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



            CreateMap<Employee, Employee>();
         
            CreateMap<Employee, EmployeeDTO>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom<EmployeeRolesResolver>())
             .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
             .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.Branch_Id))
             .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash))
             .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber));


            CreateMap<EmployeeDTO, Employee>()
             .ForMember(dest => dest.Branch_Id, opt => opt.MapFrom(src => src.BranchId))
             .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
             .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
             .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));
        }
    }


    public class EmployeeRolesResolver : IValueResolver<Employee, EmployeeDTO, List<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public EmployeeRolesResolver(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public List<string> Resolve(Employee source, EmployeeDTO destination, List<string> destMember, ResolutionContext context)
        {
            var roles = _userManager.GetRolesAsync(source).Result;

            return roles.ToList();
        }
    }

    //public class AssignRolesResolver : IMappingAction<EmployeeDTO, Employee>
    //{
    //    private readonly UserManager<ApplicationUser> _userManager;

    //    public AssignRolesResolver(UserManager<ApplicationUser> userManager)
    //    {
    //        _userManager = userManager;
    //    }

    //    public async Task ProcessAsync(EmployeeDTO source, Employee destination, ResolutionContext context)
    //    {
    //        var currentRoles = await _userManager.GetRolesAsync(destination);
    //        await _userManager.RemoveFromRolesAsync(destination, currentRoles);

    //        var result = await _userManager.AddToRolesAsync(destination, source.Roles);
    //        if (!result.Succeeded)
    //        {
    //            // Handle role assignment failure
    //            throw new Exception($"Failed to assign roles to user '{destination.UserName}'.");
    //        }
    //    }

    //    // Implement the IMappingAction interface
    //    public void Process(EmployeeDTO source, Employee destination, ResolutionContext context)
    //    {
    //        ProcessAsync(source, destination, context).GetAwaiter().GetResult();
    //    }
    //}


}
