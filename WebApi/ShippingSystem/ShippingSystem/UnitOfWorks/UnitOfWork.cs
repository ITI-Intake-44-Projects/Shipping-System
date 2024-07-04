using ShippingSystem.Models;
using ShippingSystem.Repositories;

namespace ShippingSystem.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShippingContext dbContext;

        IGovernateRepository governateRepository;

        ICityRepository cityRepository;

        IOrderRepository orderRepository;

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

        public ICityRepository CityRepository
        {
            get
            {
                if (cityRepository == null)
                {
                    cityRepository = new CityRepository(dbContext);
                }

                return cityRepository;
            }
        }

        public IOrderRepository OrderRepository
        {
            get 
            { 
                if(orderRepository == null)
                {
                    orderRepository = new OrderRepository(dbContext); 
                }
                return orderRepository;
            
            }
        }

        public async Task<int> Save()
        {
            return await dbContext.SaveChangesAsync();
        }



    }
}
