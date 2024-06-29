using ShippingSystem.Repositories;

namespace ShippingSystem.UnitOfWorks
{
    public interface IUnitOfWork
    {
        public IGovernateRepository GovernateRepository { get;}

        Task<int> Save();
    }
}
