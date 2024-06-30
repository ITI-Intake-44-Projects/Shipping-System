using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ShippingSystem.DTOs;
using ShippingSystem.Models;
using ShippingSystem.Repositories;
using ShippingSystem.UnitOfWorks;

public class EmployeeService
{
  
    private readonly IUnitOfWork unit;
    private readonly IMapper mapper;
    private readonly UserManager<ApplicationUser> userManager;

    public EmployeeService(IUnitOfWork _unit,IMapper _mapper,UserManager<ApplicationUser> _userManager)
    {
        unit = _unit;
        mapper = _mapper;
        userManager = _userManager;
    }

    public async Task<List<EmployeeDTO>> GetAllEmployees()
    {
        var employees = await unit.EmployeeRepository.GetAll();
        return mapper.Map<List<EmployeeDTO>>(employees);
    }

    public async Task<EmployeeDTO> GetEmployeeById(string id)
    {
        var employee = await unit.EmployeeRepository.GetById(id);

        if(employee == null) 
        {
            return null;
        }

        return mapper.Map<EmployeeDTO>(employee);
    }

    public async Task AddEmployee(EmployeeDTO employeeDto)
    {
        var employee = mapper.Map<Employee>(employeeDto);

        var result = await userManager.CreateAsync(employee, employeeDto.Password); // Replace with appropriate password handling
        if (!result.Succeeded)
        {
            return;
        }

       // Assign roles to the new employee

        var rolesResult = await userManager.AddToRolesAsync(employee, employeeDto.Roles);
        if (!rolesResult.Succeeded)
        {
            throw new Exception($"Failed to assign roles to user '{employee.UserName}'.");
        }

        // No need to await AssertConfigurationIsValid because it returns void
        //mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }

    public async Task<IdentityResult> UpdateEmployee(string id ,EmployeeDTO employeeDto)
    {
        var employee = await unit.EmployeeRepository.GetById(id);

        var current_roles = await userManager.GetRolesAsync(employee);

        await userManager.RemoveFromRolesAsync(employee,current_roles);

        var resultRole = await userManager.AddToRolesAsync(employee, employeeDto.Roles);
        if(!resultRole.Succeeded) 
        {
           throw new Exception($"Failed to assign roles to user '{employeeDto.FullName}'.");
        }
        employee.FullName = employeeDto.FullName;
        employee.UserName = employeeDto.UserName;
        employee.Email = employeeDto.Email;
        //employee.PasswordHash = employeeDto.Password;
        employee.PhoneNumber = employeeDto.Phone;
        if(employee.Branch != null  )
        {
            employee.Branch.Id = employeeDto.BranchId ?? employee.Branch.Id;            
        }
        else if (employee.Branch == null && employeeDto.BranchId.HasValue)
        {
            employee.Branch = new Branch { Id = employeeDto.BranchId.Value };
        }
        employee.Status = employeeDto.Status;


        var result = await userManager.UpdateAsync(employee);

        //await unit.EmployeeRepository.Update(employee);
        //await unit.Save();
        if (!string.IsNullOrEmpty(employeeDto.Password))
        {
            var token = await userManager.GeneratePasswordResetTokenAsync(employee);
            var passwordResult = await userManager.ResetPasswordAsync(employee, token, employeeDto.Password);
            if (!passwordResult.Succeeded)
            {
                throw new Exception($"Failed to update password for user '{employeeDto.FullName}'.");
            }
        }
        return result;
    }

    public async Task<bool> DeleteEmployee(string id)
    {
        var employee = await unit.EmployeeRepository.GetById(id);

        if(employee == null)
        {
            return false;
        }
        await unit.EmployeeRepository.Delete(id);

        await unit.EmployeeRepository.Save();

        return true;
    }
}
