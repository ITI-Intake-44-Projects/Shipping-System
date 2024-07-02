using Microsoft.EntityFrameworkCore;
using ShippingSystem.Models;

namespace ShippingSystem.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>,IEmployeeRepository
    {
        public EmployeeRepository(ShippingContext _db) : base(_db) { }

        public async  Task<IEnumerable<Employee>> GetActiveEmployee()
        {
            return await db.Employees.Where(e=>e.IsDeleted == false).ToListAsync();
        }

        public async Task<Employee> GetEmployeeByEmail(string email)
        {
           return await  db.Employees.FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<Employee> GetEmployeeByName(string name)
        {
            return await db.Employees.FirstOrDefaultAsync(e => e.FullName == name);
        }
    }
}
