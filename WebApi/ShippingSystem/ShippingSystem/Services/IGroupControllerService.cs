using ShippingSystem.DTOs.Groups;
using ShippingSystem.Models;

namespace ShippingSystem.Services
{
    public interface IGroupControllerService
    {
        public Task<List<GroupResponseDTO>> GetAllGroupsAsync(int pageNumber, int pageSize);
        public Task<GroupResponseDTO> GetGroupByIdAsync(string id);
        public Task<Group?> GetGroupByNameAsync(string name);
        public Task<GroupResponseDTO> AddGroupAsync(string groupName);
        public Task UpdateGroupAsync(Group group);
        public Task DeleteGroupAsync(string id);
        public Task Save();
    }
}