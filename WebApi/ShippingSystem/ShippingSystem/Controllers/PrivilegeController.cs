using Microsoft.AspNetCore.Mvc;
using ShippingSystem.Services;

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

    }
}