using ShippingSystem.Models;

namespace ShippingSystem.UnitOfWork
{
    public class unitOfWork : IUnitOfWork
    {
        private readonly ShippingContext dbContext;

        public unitOfWork(ShippingContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> Save()
        {
            return await dbContext.SaveChangesAsync();
        }



    }
}
