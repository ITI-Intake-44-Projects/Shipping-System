using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShippingSystem.DTOs.Groups;
using ShippingSystem.Models;
using ShippingSystem.Repositories;
using ShippingSystem.Services;
using ShippingSystem.UnitOfWorks;

namespace ShippingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupControllerService groupControllerService;

        public GroupController(IGroupControllerService groupControllerService)
        {
            this.groupControllerService = groupControllerService;
        }

        /// <summary>
        /// get all groups with pagination
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllGroupsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var groups = await groupControllerService.GetAllGroupsAsync(pageNumber, pageSize);
                return Ok(groups);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// get group by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var groupResponse = await groupControllerService.GetGroupByIdAsync(id);
                if (groupResponse == null)
                    return Ok("No such group with the provided Id");
                return Ok(groupResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// get group by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var group = await groupControllerService.GetGroupByNameAsync(name);
                if (group == null) 
                    return NotFound("Role doesn't exist");
                return Ok(group);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// add a new group
        /// </summary>
        /// <param name="groupDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(GroupDTO groupDTO)
        {
            try
            {
                var existingGroup = await groupControllerService.GetGroupByNameAsync(groupDTO.Name);
                if (existingGroup != null)
                {
                    return BadRequest("Role already exists");
                }
                var groupResponse =  await groupControllerService.AddGroupAsync(groupDTO);
                return CreatedAtAction(nameof(GetById), new { id = groupResponse.Id }, groupResponse);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// update group data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="groupDTO"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(string id, GroupDTO groupDTO)
        {
            try
            {
                var existingGroup = await groupControllerService.GetGroupByIdAsync(id);

                if (existingGroup == null)
                {
                    return NotFound("Old role doesn't exist");
                }
                await groupControllerService.UpdateGroupAsync(existingGroup, groupDTO);
                return Ok("Role Updated Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// delete group by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var group = await groupControllerService.GetGroupByIdAsync(id);
            if (group == null)
            {
                return NotFound("Role doesn't exist");
            }
            await groupControllerService.DeleteGroupAsync(group);
            return Ok($"Role: {group.Name} has been deleted successfully");
        }
    }
}