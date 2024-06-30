using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShippingSystem.DTOs.Groups;
using ShippingSystem.Models;
using ShippingSystem.Repositories;
using ShippingSystem.UnitOfWorks;

namespace ShippingSystem.Services
{
    public class GroupControllerService : IGroupControllerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GroupControllerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<GroupResponseDTO>> GetAllGroupsAsync(int pageNumber, int pageSize)
        {
            var groups = await unitOfWork.GroupRepository.GetGroupsAsync(pageNumber, pageSize);
            return mapper.Map<List<GroupResponseDTO>>(groups);
        }

        public async Task<GroupResponseDTO> GetGroupByIdAsync(string id)
        {
            var group = await unitOfWork.GroupRepository.GetById(id);
            return mapper.Map<GroupResponseDTO>(group);
        }

        public async Task<Group?> GetGroupByNameAsync(string name)
        {
            return await unitOfWork.GroupRepository.GetGroupByNameAsync(name); ;
        }

        public async Task<GroupResponseDTO> AddGroupAsync(string groupName)
        {
            var group = new Group
            {
                Id = Guid.NewGuid().ToString(),
                DateAdded = DateTime.Now,
                Name = groupName,
                NormalizedName = groupName.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
            };
            await unitOfWork.GroupRepository.Add(group);
            await unitOfWork.GroupRepository.Save();
            return mapper.Map<GroupResponseDTO>(group);
        }

        public async Task UpdateGroupAsync(Group group)
        {
            try
            {
                await unitOfWork.GroupRepository.Update(group);
                await unitOfWork.GroupRepository.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbUpdateConcurrencyException("Concurrency conflict occurred.", ex);
            }
        }

        public async Task DeleteGroupAsync(string id)
        {
            await unitOfWork.GroupRepository.Delete(id);
        }

        public async Task Save()
        {
            await unitOfWork.Save();
        }
    }
}