using ShippingSystem.Repositories;

namespace ShippingSystem.UnitOfWorks
{
    public interface IUnitOfWork
    {
        public IGovernateRepository GovernateRepository { get;}
        public ICityRepository CityRepository { get;}
        Task<int> Save();
    }
}
