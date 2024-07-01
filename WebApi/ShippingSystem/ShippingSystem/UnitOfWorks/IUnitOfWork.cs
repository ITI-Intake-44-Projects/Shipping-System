using ShippingSystem.Repositories;

namespace ShippingSystem.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IEmployeeRepository EmployeeRepository {get;}

        Task<int> Save();
    }
}
