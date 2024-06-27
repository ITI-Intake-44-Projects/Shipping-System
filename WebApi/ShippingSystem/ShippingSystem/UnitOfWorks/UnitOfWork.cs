using ShippingSystem.Models;

namespace ShippingSystem.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShippingContext dbContext;

        public UnitOfWork(ShippingContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> Save()
        {
            return await dbContext.SaveChangesAsync();
        }



    }
}
