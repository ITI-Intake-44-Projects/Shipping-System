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

        public RepresentativesController(ShippingContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RepresentativeDtoGET>>> GetRepresentatives()
        {
            var representatives = await _context.Representatives
                .Select(r => new RepresentativeDtoGET
                {
                    Id = r.Id,
                    FullName = r.FullName,
                    Email = r.Email,
                    PhoneNumber = r.PhoneNumber,
                    Address = r.Address,
                    Password = r.PasswordHash,
                    CompanyOrderPrecentage = r.CompanyOrderPrecentage,
                    SalePrecentage = r.SalePrecentage,
                    Branch_Id = r.Branch.Id,
                    BranchName = r.Branch.Name,
                    LockoutEnabled = r.LockoutEnabled,
                    Governorates = r.RepresentativeGovernates
                        .Select(g => new RepresentativeGovernateDTO
                        {
                            Id = g.Governate.Id,
                            Name = g.Governate.Name,
                            Status = g.Governate.Status
                        })
                        .ToList()
                })
                .ToListAsync();

            return Ok(representatives);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Representative>> GetRepresentative(string id)
        {
            var representative = await _context.Representatives.FindAsync(id);

            if (representative == null)
            {
                return NotFound();
            }

            return representative;
        }

        [HttpPost]
        public async Task<ActionResult<Representative>> PostRepresentative(RepresentativeDTO representativeDto)
        {
            var representative = new Representative
            {
                FullName = representativeDto.FullName,
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

            _context.Representatives.Add(representative);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRepresentative", new { id = representative.Id }, representative);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRepresentative(string id, RepresentativeDTO representativeDto)
        {
            if (id != representativeDto.id)
            {
                return BadRequest();
            }

            var representative = await _context.Representatives
                .Include(r => r.RepresentativeGovernates)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (representative == null)
            {
                return NotFound();
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

            _context.Representatives.Update(representative);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RepresentativeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRepresentative(string id)
        {
            var representative = await _context.Representatives.FindAsync(id);
            if (representative == null)
            {
                return NotFound();
            }

            _context.Representatives.Remove(representative);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RepresentativeExists(string id)
        {
            return _context.Representatives.Any(e => e.Id == id);
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
