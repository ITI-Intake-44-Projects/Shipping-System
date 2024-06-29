using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShippingSystem.DTOs.Privileges;
using ShippingSystem.Services;

namespace ShippingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivilegeController : ControllerBase
    {
        private readonly IPrivilegeService privilegeService;

        public PrivilegeController(IPrivilegeService privilegeService)
        {
            this.privilegeService = privilegeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var privileges = await privilegeService.GetAllPrivilegesAsync();
            return Ok(privileges);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var privilege = await privilegeService.GetPrivilegeByIdAsync(id);
            if (privilege == null) return NotFound();
            return Ok(privilege);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PrivilegeDTO privilegeDTO)
        {
            await privilegeService.AddPrivilegeAsync(privilegeDTO);
            return CreatedAtAction(nameof(GetById), new { id = privilegeDTO.Id }, privilegeDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PrivilegeDTO privilegeDTO)
        {
            if (id != privilegeDTO.Id) return BadRequest();
            await privilegeService.UpdatePrivilegeAsync(privilegeDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await privilegeService.DeletePrivilegeAsync(id);
            return NoContent();
        }
    }
}