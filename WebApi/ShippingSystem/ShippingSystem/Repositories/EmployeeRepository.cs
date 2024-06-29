using ShippingSystem.Models;

namespace ShippingSystem.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>,IEmployeeRepository
    {
        public EmployeeRepository(ShippingContext _db) : base(_db) { }
        
    }
}
