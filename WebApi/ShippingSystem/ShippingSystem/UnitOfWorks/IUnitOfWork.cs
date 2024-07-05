using ShippingSystem.Repositories;

namespace ShippingSystem.UnitOfWorks
{
    public interface IUnitOfWork
    {
        public IGovernateRepository GovernateRepository { get;}

        public ICityRepository CityRepository { get;}

        public IOrderRepository OrderRepository { get;}

        IEmployeeRepository EmployeeRepository {get;}

        Task<int> Save();
    }
}
