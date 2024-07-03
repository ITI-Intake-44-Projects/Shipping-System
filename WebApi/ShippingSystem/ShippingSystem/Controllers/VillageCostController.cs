using Microsoft.AspNetCore.Mvc;
using ShippingSystem.DTOs;
using ShippingSystem.DTOs.VillageCost;
using ShippingSystem.Models;
using ShippingSystem.Services;
using System.Threading.Tasks;

namespace ShippingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillageCostController : ControllerBase
    {
        private readonly VillageCostService _villageCostService;

        public VillageCostController(VillageCostService villageCostService)
        {
            _villageCostService = villageCostService;
        }

        // POST: api/VillageCost
        [HttpPost]
        public async Task<ActionResult<VillageCost>> CreateVillageCost([FromBody] VillageCostDTO villageCostDto)
        {
            if (villageCostDto == null)
            {
                return BadRequest("VillageCostDTO is null");
            }

            var villageCost = await _villageCostService.AddVillageCost(villageCostDto);

            return CreatedAtAction(nameof(CreateVillageCost), new { id = villageCost.Id }, villageCost);
        }
    }
}
