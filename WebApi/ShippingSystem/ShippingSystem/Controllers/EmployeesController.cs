using Microsoft.AspNetCore.Mvc;
using ShippingSystem.DTOs;
using ShippingSystem.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShippingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService employeeService;

        public EmployeesController(EmployeeService _employeeService)
        {
            employeeService = _employeeService;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {

            var employees = await employeeService.GetAllEmployees();

            if(employees == null) 
            {
                return NotFound(new { message = "no employees found" });
            
            }
            return Ok(employees);
        }

        // GET: api/Employees/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(string id)
        {
            var employee = await employeeService.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound(new { message = "no employee found with this id" });
            }

            return Ok(employee);
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> AddEmployee (EmployeeDTO employeeDto)
        {
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            await employeeService.AddEmployee(employeeDto);

            return Ok(new { message = "employee added successfully" });
        }

        // PUT: api/Employees/{id}
        [HttpPut("{id:alpha}")]
        public async Task<IActionResult> UpdateEmployee(string id, EmployeeDTO employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            await employeeService.UpdateEmployee(employeeDto);

            return Ok(new { message = "employee updated successfully" });
        }

        // DELETE: api/Employees/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var employee = await employeeService.DeleteEmployee(id);

            if (employee == null)
            {
                return NotFound(new { message = "no employee found with this id" });
            }

            await employeeService.DeleteEmployee(id);
            return NoContent();
        }
    }
}
