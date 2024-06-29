using AutoMapper;
using ShippingSystem.DTOs;
using ShippingSystem.Models;
using ShippingSystem.Repositories;
using ShippingSystem.UnitOfWorks;

public class EmployeeService
{
  
    private readonly IUnitOfWork unit;
    private readonly IMapper mapper;

    public EmployeeService(IUnitOfWork _unit,IMapper _mapper)
    {
        unit = _unit;
        mapper = _mapper;
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
        
        await unit.EmployeeRepository.Add(employee);

        await unit.EmployeeRepository.Save();
    }

    public async Task<EmployeeDTO> UpdateEmployee(string id, EmployeeDTO employeeDto)
    {
        var employee = mapper.Map<Employee>(employeeDto);

        await unit.EmployeeRepository.Update(employee);

        await unit.EmployeeRepository.Save();
    }

    public async Task<bool> DeleteEmployee(string id)
    {
        // Find employee by string ID in a more manual way
        var employee = await _repository.GetAll();
        var foundEmployee = employee.FirstOrDefault(e => e.Id == id);

        if (foundEmployee == null)
        {
            return false;
        }

        await _repository.Delete(foundEmployee);
        await _repository.Save();
        return true;
    }
}
