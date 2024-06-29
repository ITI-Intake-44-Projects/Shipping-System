using ShippingSystem.Models;
using ShippingSystem.UnitOfWorks;

namespace ShippingSystem.Repositories
{
    public class GroupRepository : GenericRepository<Group>, IGroupRepository
    {
        private readonly UnitOfWork unitOfWork;

        public GroupRepository(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Group> GetGroupByGroupNameAsync(string groupName)
        {
            
        }
    }
}
