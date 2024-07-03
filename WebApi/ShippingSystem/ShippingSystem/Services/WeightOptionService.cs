using ShippingSystem.DTOs.WeightOption;
using ShippingSystem.Models;
using ShippingSystem.Repositories;
using System.Threading.Tasks;

namespace ShippingSystem.Services
{
    public class WeightOptionService
    {
        private readonly IGenericRepository<WeightOption> _repository;

        public WeightOptionService(IGenericRepository<WeightOption> repository)
        {
            _repository = repository;
        }

        public async Task<WeightOption> AddWeightOption(WeightOptionDTO weightOptionDto)
        {
            var weightOption = new WeightOption
            {
                AdditionalKgPrice = weightOptionDto.AdditionalKgPrice,
                MaximumWeight = weightOptionDto.MaximumWeight
            };

            await _repository.Add(weightOption);
            await _repository.Save();

            return weightOption;
        }
    }
}
