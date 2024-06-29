using AutoMapper;
using ShippingSystem.DTOs;
using ShippingSystem.Models;
using ShippingSystem.Repositories;

public class EmployeeService
{
    private readonly IGenericRepository<Employee> _repository;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Branch> _branchRepository;

    public EmployeeService(IGenericRepository<Employee> repository, IMapper mapper, IGenericRepository<Branch> branchRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _branchRepository = branchRepository;
    }

    public async Task<List<EmployeeDTO>> GetAllEmployees()
    {
        var employees = await _repository.GetAll();
        return _mapper.Map<List<EmployeeDTO>>(employees);
    }

    public async Task<EmployeeDTO> GetEmployeeById(string id)
    {
        // Find employee by string ID in a more manual way
        var employee = await _repository.GetAll();
        var foundEmployee = employee.FirstOrDefault(e => e.Id == id);

        return _mapper.Map<EmployeeDTO>(foundEmployee);
    }

    public async Task<EmployeeDTO> CreateEmployee(EmployeeDTO employeeDto)
    {
        var employee = _mapper.Map<Employee>(employeeDto);
        var branch = await _branchRepository.GetById(employeeDto.BranchId);
        if (branch == null)
        {
            throw new KeyNotFoundException("Branch not found.");
        }

        employee.Branch = branch;
        await _repository.Add(employee);
        await _repository.Save();
        return _mapper.Map<EmployeeDTO>(employee);
    }

    public async Task<EmployeeDTO> UpdateEmployee(string id, EmployeeDTO employeeDto)
    {
        // Find employee by string ID in a more manual way
        var employee = await _repository.GetAll();
        var foundEmployee = employee.FirstOrDefault(e => e.Id == id);

        if (foundEmployee == null)
        {
            return null;
        }

        _mapper.Map(employeeDto, foundEmployee);
        var branch = await _branchRepository.GetById(employeeDto.BranchId);
        if (branch == null)
        {
            throw new KeyNotFoundException("Branch not found.");
        }

        foundEmployee.Branch = branch;
        await _repository.Update(foundEmployee);
        await _repository.Save();
        return _mapper.Map<EmployeeDTO>(foundEmployee);
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
