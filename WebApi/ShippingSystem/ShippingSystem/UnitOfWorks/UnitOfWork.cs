using AutoMapper;
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

       
        private IEmployeeRepository employeeRepository;

        public UnitOfWork(ShippingContext dbContext )
        { 
            this.dbContext = dbContext;
          
        }

        public IEmployeeRepository EmployeeRepository
        {
            get
            {
                if (employeeRepository == null) 
                {
                    employeeRepository = new EmployeeRepository(dbContext);
                }

                return employeeRepository;
                
            }
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
