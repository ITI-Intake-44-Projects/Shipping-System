using Microsoft.EntityFrameworkCore;
using ShippingSystem.Models;
using ShippingSystem.Repositories;

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

        private GroupRepository groupRepository;
        public GroupRepository GroupRepository
        {
            get
            {
                return groupRepository ?? (groupRepository = new GroupRepository(dbContext));
            }
        }

        private PrivilegeRepository privilegeRepository;
        public PrivilegeRepository PrivilegeRepository
        {
            get
            {
                return privilegeRepository ?? (privilegeRepository = new PrivilegeRepository(dbContext));
            }
        }

        private GroupPrivilegeRepository groupPrivilegeRepository;
        public GroupPrivilegeRepository GroupPrivilegeRepository
        {
            get { return groupPrivilegeRepository ??= new GroupPrivilegeRepository(dbContext); }
        }
    }
}
