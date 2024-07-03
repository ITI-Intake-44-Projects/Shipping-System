using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShippingSystem.DTOs.Representatives;
using ShippingSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShippingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepresentativesController : ControllerBase
    {
        private readonly ShippingContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public RepresentativesController(ShippingContext context,UserManager<ApplicationUser> _userManager,IMapper _mapper)
        {
            _context = context;
            userManager = _userManager;
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RepresentativeDTO>>> GetRepresentatives()
        {
            var representatives = await _context.Representatives.Where(r => r.IsDeleted == false).ToListAsync();

            if (representatives == null)
            {
                return NotFound(new {message ="no representatives found"});
            }

            var representativesDto = mapper.Map<IEnumerable<RepresentativeDTO>>(representatives);

            return Ok(representativesDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Representative>> GetRepresentative(string id)
        {
            var representative = await _context.Representatives.FindAsync(id);

            if (representative == null)
            {
                return NotFound();
            }

            var representativeDto = new RepresentativeDTO()
            {
                FullName = representative.FullName,
                PhoneNumber=representative.PhoneNumber,
                Email = representative.Email,
                Address = representative.Address,
                CompanyOrderPrecentage = representative.CompanyOrderPrecentage,
                SalePrecentage = representative.SalePrecentage,
                Branch_Id = representative.Branch.Id,
                Password = representative.PasswordHash
            };


            return Ok(representativeDto);
        }

        [HttpPost]
        public async Task<ActionResult<Representative>> PostRepresentative(RepresentativeDTO representativeDto)
        {
            var representative = new Representative
            {
                FullName = representativeDto.FullName,
                UserName = representativeDto.UserName,
                PhoneNumber = representativeDto.PhoneNumber,
                Email = representativeDto.Email,
                Address = representativeDto.Address,
                CompanyOrderPrecentage = representativeDto.CompanyOrderPrecentage,
                SalePrecentage = representativeDto.SalePrecentage,
                Branch_Id = representativeDto.Branch_Id,
                PasswordHash = representativeDto.Password
            };

            foreach (var governateId in representativeDto.GovernateIds)
            {
                representative.RepresentativeGovernates.Add(new RepresentativeGovernate
                {
                    Governate_Id = governateId,
                    Representative = representative
                });
            }
            var result = await userManager.CreateAsync(representative, representativeDto.Password); 
            if (!result.Succeeded)
            {
                return BadRequest(result);
            }
            

            var rolesResult = await userManager.AddToRoleAsync(representative,"representative");

            if (!rolesResult.Succeeded)
            {
                return BadRequest(rolesResult);
            }
           

            return Ok(new { message = "representative added successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRepresentative(string id, RepresentativeDTO representativeDto)
        {
            if (id != representativeDto.id)
            {
                return BadRequest(new {message = "not valid id"});
            }

            var representative = await _context.Representatives
                .Include(r => r.RepresentativeGovernates)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (representative == null)
            {
                return NotFound(new { message = "representative not found" });
            }

            representative.FullName = representativeDto.FullName;
            representative.PhoneNumber = representativeDto.PhoneNumber;
            representative.Email = representativeDto.Email;
            representative.Address = representativeDto.Address;
            representative.CompanyOrderPrecentage = representativeDto.CompanyOrderPrecentage;
            representative.SalePrecentage = representativeDto.SalePrecentage;
            representative.Branch_Id = representativeDto.Branch_Id;
            representative.PasswordHash = representativeDto.Password;

            // Update RepresentativeGovernates
            representative.RepresentativeGovernates.Clear();
            foreach (var governateId in representativeDto.GovernateIds)
            {
                representative.RepresentativeGovernates.Add(new RepresentativeGovernate
                {
                    Governate_Id = governateId,
                    Representative = representative
                });
            }

           var result = await userManager.UpdateAsync(representative);

            if(!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }


            return Ok(new {message = "representative updated successfully"});
            //_context.Representatives.Update(representative);

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!RepresentativeExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRepresentative(string id)
        {
            var representative = await _context.Representatives.FindAsync(id);

            if (representative == null)
            {
                return NotFound();
            }

            representative.IsDeleted = true;

            return Ok(new {message =" representative deleted successfully"});
        }

      

        [HttpGet("governorates")]
        public async Task<ActionResult<IEnumerable<RepresentativeGovernateDTO>>> GetGovernorates()
        {
            var governorates = await _context.Governates
                .Select(g => new RepresentativeGovernateDTO
                {
                    Id = g.Id,
                    Name = g.Name,
                    Status = g.Status
                })
                .ToListAsync();

            return Ok(governorates);
        }
        [HttpGet("branches")]
        public async Task<ActionResult<IEnumerable<RepresentativeGovernateDTO>>> GetBranches()
        {
            var branches = await _context.Branches
                .Select(g => new RepresentativeGovernateDTO
                {
                    Id = g.Id,
                    Name = g.Name,
                    Status = g.Status
                })
                .ToListAsync();

            return Ok(branches);
        }
    }
}
