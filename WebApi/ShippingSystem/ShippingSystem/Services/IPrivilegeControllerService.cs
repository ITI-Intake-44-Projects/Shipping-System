using ShippingSystem.Models;

namespace ShippingSystem.Services
{
    public interface IPrivilegeControllerService
    {
        public Task<IEnumerable<Privilege>> GetAllPrivilegesAsync(int pageNumber, int pageSize);
    }
}