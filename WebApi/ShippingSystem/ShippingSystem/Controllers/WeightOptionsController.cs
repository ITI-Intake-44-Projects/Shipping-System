using Microsoft.AspNetCore.Mvc;
using ShippingSystem.DTOs.WeightOption;
using ShippingSystem.Models;
using ShippingSystem.Services;
using System.Threading.Tasks;

namespace ShippingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeightOptionsController : ControllerBase
    {
        private readonly WeightOptionService _weightOptionService;

        public WeightOptionsController(WeightOptionService weightOptionService)
        {
            _weightOptionService = weightOptionService;
        }

        // POST: api/WeightOptions
        [HttpPost]
        public async Task<ActionResult<WeightOption>> CreateWeightOption([FromBody] WeightOptionDTO weightOptionDto)
        {
            if (weightOptionDto == null)
            {
                return BadRequest("WeightOptionDTO is null");
            }

            var createdWeightOption = await _weightOptionService.AddWeightOption(weightOptionDto);

            return CreatedAtAction(nameof(CreateWeightOption), new { id = createdWeightOption.Id }, createdWeightOption);
        }
    }
}
