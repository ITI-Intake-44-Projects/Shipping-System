using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShippingSystem.DTOs.Groups;
using ShippingSystem.DTOs.Privileges;
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
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
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
                var group = await groupControllerService.GetGroupByIdAsync(id);
                if (group == null)
                    return Ok("Role doesn't exist");
                return Ok(group);
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
        public async Task<IActionResult> Add(string groupName)
        {
            try
            {
                var existingGroup = await groupControllerService.GetGroupByNameAsync(groupName);
                if (existingGroup != null)
                {
                    return BadRequest("Role already exists");
                }
                var groupResponse =  await groupControllerService.AddGroupAsync(groupName);
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
        public async Task<IActionResult> Update(string oldGroupName, string newGroupName)
        {
            try
            {
                var existingGroup = await groupControllerService.GetGroupByNameAsync(oldGroupName);

                if (existingGroup == null)
                {
                    return NotFound("Old role doesn't exist");
                }

                existingGroup.Name = newGroupName;
                existingGroup.NormalizedName = newGroupName.ToUpper();

                await groupControllerService.UpdateGroupAsync(existingGroup);
                return Ok();
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
        [HttpDelete("{groupName}")]
        public async Task<IActionResult> Delete(string groupName)
        {
            var existingGroup = await groupControllerService.GetGroupByNameAsync(groupName);
            if(existingGroup == null)
            {
                return NotFound("Role doesn't exist");
            }
            await groupControllerService.DeleteGroupAsync(existingGroup.Id);
            await groupControllerService.Save();
            return Ok($"Role: {existingGroup.Name} has been deleted successfully");
        }
    }
}