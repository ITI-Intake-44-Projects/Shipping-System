using ShippingSystem.Models;
using ShippingSystem.Repositories;

namespace ShippingSystem.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShippingContext dbContext;

        IGovernateRepository governateRepository;

        public UnitOfWork(ShippingContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IGovernateRepository GovernateRepository
        {
            get
            {
                if (governateRepository == null) 
                {
                    governateRepository = new GovernateRepository(dbContext);
                }

                return governateRepository;
            }
        }

        public async Task<int> Save()
        {
            return await dbContext.SaveChangesAsync();
        }



    }
}
