using AutoMapper;
using ShippingSystem.Models;
using ShippingSystem.Repositories;

namespace ShippingSystem.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShippingContext dbContext;
       
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


        public async Task<int> Save()
        {
            return await dbContext.SaveChangesAsync();
        }



    }
}
