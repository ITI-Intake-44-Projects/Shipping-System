using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShippingSystem.DTOs.Groups;
using ShippingSystem.DTOs.Privileges;
using ShippingSystem.Services;

namespace ShippingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService groupService;

        public GroupController(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var groups = await groupService.GetAllGroupsAsync();
            return Ok(groups);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var group = await groupService.GetGroupByIdAsync(id);
            if (group == null) return NotFound();
            return Ok(group);
        }

        [HttpPost]
        public async Task<IActionResult> Add(GroupDTO groupDTO)
        {
            await groupService.AddGroupAsync(groupDTO);
            return CreatedAtAction(nameof(GetById), new { id = groupDTO.Id }, groupDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, GroupDTO groupDTO)
        {
            if (id != groupDTO.Id) return BadRequest();
            await groupService.UpdateGroupAsync(groupDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await groupService.DeleteGroupByIdAsync(id);
            return NoContent();
        }
    }
}