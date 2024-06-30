using AutoMapper;
using ShippingSystem.DTOs.Privileges;
using ShippingSystem.Models;
using ShippingSystem.Repositories;
using ShippingSystem.UnitOfWorks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShippingSystem.Services
{
    public class PrivilegeControllerService : IPrivilegeControllerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PrivilegeControllerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<Privilege>> GetAllPrivilegesAsync(int pageNumber, int pageSize)
        {
            return await unitOfWork.PrivilegeRepository.GetAll();
        }

        public async Task<Privilege?> GetPrivilegeByIdAsync(int id)
        {
            return await unitOfWork.PrivilegeRepository.GetById(id);
        }

        public async Task<Privilege?> GetPrivilegeByNameAsync(string privilegeName)
        {
            return await unitOfWork.PrivilegeRepository.GetPrivilegeByNameAsync(privilegeName); ;
        }

        public async Task<Privilege?> AddPrivilegeAsync(string privilegeName)
        {
            //    var privilege = new Privilege
            //    {
            //        Name = privilegeName,
            //        Add = false,
            //        Delete = false,
            //        Update = false,
            //        View = false,
            //    }
            //    await unitOfWork.PrivilegeRepository.Add(privilege);
            //    await unitOfWork.PrivilegeRepository.Save();
            throw new NotImplementedException();
        }

        public async Task UpdatePrivilegeAsync(Privilege privilege)
        {
            throw new NotImplementedException();
        }

        public async Task DeletePrivilegeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Save()
        {
            throw new NotImplementedException();
        }
    }
}
