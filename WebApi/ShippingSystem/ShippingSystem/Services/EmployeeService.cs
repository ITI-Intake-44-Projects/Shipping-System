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

    public async Task UpdateEmployee(EmployeeDTO employeeDto)
    {
        var employee = mapper.Map<Employee>(employeeDto);

        await unit.EmployeeRepository.Update(employee);

        await unit.EmployeeRepository.Save();
    }

    public async Task<bool> DeleteEmployee(string id)
    {
        var employee = unit.EmployeeRepository.GetById(id);

        if(employee == null)
        {
            return false;
        }
        await unit.EmployeeRepository.Delete(id);

        await unit.EmployeeRepository.Save();

        return true;
    }
}
