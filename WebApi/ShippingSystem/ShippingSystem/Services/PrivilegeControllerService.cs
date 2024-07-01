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

        public async Task<IEnumerable<Privilege?>> GetAllPrivilegesAsync(int pageNumber, int pageSize)
        {
            return await unitOfWork.PrivilegeRepository.GetPrivilegesAsync(pageNumber, pageSize);
        }
    }
}
