using ShippingSystem.Models;

namespace ShippingSystem.Repositories
{
    public interface IGroupRepository : IGenericRepository<Group>
    {
        Task<Group> GetGroupByGroupNameAsync(string groupName);
    }
}
