using ShippingSystem.DTOs;
using ShippingSystem.DTOs.VillageCost;
using ShippingSystem.Models;
using ShippingSystem.Repositories;
using System.Threading.Tasks;

namespace ShippingSystem.Services
{
    public class VillageCostService
    {
        private readonly IGenericRepository<VillageCost> _repository;

        public VillageCostService(IGenericRepository<VillageCost> repository)
        {
            _repository = repository;
        }

        public async Task<VillageCost> AddVillageCost(VillageCostDTO villageCostDto)
        {
            var villageCost = new VillageCost
            {
                Price = villageCostDto.Price
            };

            await _repository.Add(villageCost);
            await _repository.Save();

            return villageCost;
        }
    }
}
