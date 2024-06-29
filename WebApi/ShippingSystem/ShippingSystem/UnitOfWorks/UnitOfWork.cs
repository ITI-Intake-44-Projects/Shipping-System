using ShippingSystem.Models;
using ShippingSystem.Repositories;

namespace ShippingSystem.UnitOfWorks
{
    public class UnitOfWork
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

        private GenericRepository<Privilege> privilegeGenericRepository;
        public GenericRepository<Privilege> PrivilegeGenericRepository
        {
            get
            {
                return privilegeGenericRepository ?? (privilegeGenericRepository = new GenericRepository<Privilege>(dbContext));
            }
        }

        private GenericRepository<Group> groupGenericRepository;
        public GenericRepository<Group> GroupGenericRepository
        {
            get
            {
                return groupGenericRepository ?? (groupGenericRepository = new GenericRepository<Group>(dbContext));
            }
        }
    }
}
