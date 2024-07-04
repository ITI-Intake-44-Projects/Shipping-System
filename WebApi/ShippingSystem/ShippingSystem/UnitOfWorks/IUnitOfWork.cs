using ShippingSystem.Repositories;

namespace ShippingSystem.UnitOfWorks
{
    public interface IUnitOfWork
    {
        public IGovernateRepository GovernateRepository { get;}

        public ICityRepository CityRepository { get;}

        public IOrderRepository OrderRepository { get;}

        Task<int> Save();
    }
}
