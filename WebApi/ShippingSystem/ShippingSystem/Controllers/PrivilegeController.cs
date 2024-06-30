using Microsoft.AspNetCore.Mvc;
using ShippingSystem.DTOs.Privileges;
using ShippingSystem.Services;
using System.Threading.Tasks;

namespace ShippingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivilegeController : ControllerBase
    {
        private readonly IPrivilegeControllerService privilegeControllerService;

        public PrivilegeController(IPrivilegeControllerService privilegeControllerService)
        {
            this.privilegeControllerService = privilegeControllerService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
        {
            try
            {
                return Ok(await privilegeControllerService.GetAllPrivilegesAsync(pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var privilege = await privilegeControllerService.GetPrivilegeByIdAsync(id);
                if (privilege == null)
                    return Ok($"Privilege doesn't exist");
                return Ok(privilege);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var privilege = await privilegeControllerService.GetPrivilegeByNameAsync(name);
                if (privilege == null)
                    return NotFound("Privilege doesn't exist");
                return Ok(privilege);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpPost]
        public async Task<IActionResult> Add(string privilegeName)
        {
            try
            {
                var existingPrivilege = await privilegeControllerService.GetPrivilegeByNameAsync(privilegeName);
                if (existingPrivilege != null)
                {
                    return BadRequest("Privilege already exists");
                }
                var privilege = await privilegeControllerService.AddPrivilegeAsync(privilegeName);
                return CreatedAtAction(nameof(GetById), new { id = privilege.Id }, privilege);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldPrivilegeName"></param>
        /// <param name="newPrivilegeName"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(string oldPrivilegeName, string newPrivilegeName)
        {
            try
            {
                var existingPrivilege = await privilegeControllerService.GetPrivilegeByNameAsync(oldPrivilegeName);

                if (existingPrivilege == null)
                {
                    return NotFound("Old privilege doesn't exist");
                }

                existingPrivilege.Name = newPrivilegeName;

                await privilegeControllerService.UpdatePrivilegeAsync(existingPrivilege);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpDelete("{privilegeName}")]
        public async Task<IActionResult> Delete(string privilegeName)
        {
            var existingPrivilege = await privilegeControllerService.GetPrivilegeByNameAsync(privilegeName);
            if (existingPrivilege == null)
            {
                return NotFound("Role doesn't exist");
            }
            await privilegeControllerService.DeletePrivilegeAsync(existingPrivilege.Id);
            await privilegeControllerService.Save();
            return Ok($"Role: {existingPrivilege.Name} has been deleted successfully");
        }
    }
}