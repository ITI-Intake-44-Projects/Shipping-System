//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using ShippingSystem.DTOs.Merchant;
//using ShippingSystem.Models;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace ShippingSystem.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class MerchantsController : ControllerBase
//    {
//        private readonly ShippingContext _context;

//        public MerchantsController(ShippingContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Merchants
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Merchant>>> GetMerchants()
//        {
//            return await _context.Merchants.ToListAsync();
//        }

//        // GET: api/Merchants/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Merchant>> GetMerchant(int id)
//        {
//            var merchant = await _context.Merchants.FindAsync(id);

//            if (merchant == null)
//            {
//                return NotFound();
//            }

//            return merchant;
//        }
//        // POST: api/Merchants
//        [HttpPost]
//        public async Task<ActionResult<Merchant>> PostMerchant(MerchantDTO merchantDTO)
//        {
//            // Validate DTO
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            // Map DTO to Merchant entity
//            var merchant = new Merchant
//            {
//                FullName = merchantDTO.FullName,
//                Address = merchantDTO.Address,
//                Governate = merchantDTO.Governate,
//                City = merchantDTO.City,
//                StoreName = merchantDTO.StoreName,
//                SpecialPickupCost = merchantDTO.SpecialPickupCost,
//                InCompleteShippingRatio = merchantDTO.InCompleteShippingRatio
//            };

//            // Find or create Branch (assuming BranchName is unique)
//            var branch = await _context.Branches.FirstOrDefaultAsync(b => b.Name == merchantDTO.BranchName);
//            if (branch == null)
//            {
//                branch = new Branch { Name = merchantDTO.BranchName };
//                _context.Branches.Add(branch);
//                await _context.SaveChangesAsync();
//            }

//            merchant.Branch = branch;

//            // Map SpecialPrices from DTO to entity
//            foreach (var specialPriceDTO in merchantDTO.SpecialPrices)
//            {
//                var specialPrice = new SpecialPrice
//                {
//                    TransportCost = specialPriceDTO.TransportCost,
//                    Governate = specialPriceDTO.Governate,
//                    City = specialPriceDTO.City
//                };
//                merchant.SpecialPrices.Add(specialPrice);
//            }

//            // Add to context and save changes
//            _context.Merchants.Add(merchant);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction(nameof(GetMerchant), new { id = merchant.Id }, merchant);
//        }



//        // PUT: api/Merchants/5
//        // PUT: api/Merchants/5
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutMerchant(string id, Merchant merchant)
//        {
//            if (id != merchant.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(merchant).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!MerchantExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        private bool MerchantExists(string id)
//        {
//            return _context.Merchants.Any(e => e.Id == id);
//        }


//        // DELETE: api/Merchants/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteMerchant(string id)
//        {
//            var merchant = await _context.Merchants.FindAsync(id);
//            if (merchant == null)
//            {
//                return NotFound();
//            }

//            _context.Merchants.Remove(merchant);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }


//    }
//}
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShippingSystem.DTOs;
using ShippingSystem.DTOs.Merchant;
using ShippingSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShippingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantsController : ControllerBase
    {
        private readonly ShippingContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public MerchantsController(ShippingContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: api/Merchants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MerchantResponseDTO>>> GetMerchants(int page = 1, int pageSize = 10)
        {
            var merchants = await _context.Merchants
               // .Where(m => !m.IsDeleted)
                .Include(m => m.SpecialPrices)
                .Include(m => m.Branch)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var merchantDTOs = merchants.Select(merchant => new MerchantResponseDTO
            {
                Id = merchant.Id,
                FullName = merchant.FullName,
                Address = merchant.Address,
                Governate = merchant.Governate,
                Email = merchant.Email,
                Password = merchant.PasswordHash,
                PhoneNumber = merchant.PhoneNumber,
                UserName = merchant.UserName,
                City = merchant.City,
                StoreName = merchant.StoreName,
                SpecialPickupCost = (int)merchant.SpecialPickupCost,
                InCompleteShippingRatio = (int)merchant.InCompleteShippingRatio,
                BranchName = merchant.Branch?.Name,
                SpecialPrices = merchant.SpecialPrices.Select(sp => new SpecialPriceDTO
                {
                    TransportCost = sp.TransportCost,
                    Governate = sp.Governate,
                    City = sp.City
                }).ToList()
            }).ToList();

            return Ok(merchantDTOs);
        }

        // GET: api/Merchants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MerchantResponseDTO>> GetMerchant(string id)
        {
            var merchant = await _context.Merchants
           //     .Where(m => !m.IsDeleted)
                .Include(m => m.SpecialPrices)
                .Include(m => m.Branch)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (merchant == null)
            {
                return NotFound();
            }

            var merchantDTO = new MerchantResponseDTO
            {
                Id = merchant.Id,
                Email = merchant.Email,
                PhoneNumber = merchant.PhoneNumber,
                Password =merchant.PasswordHash,
                UserName = merchant.UserName,
                FullName = merchant.FullName,
                Address = merchant.Address,
                Governate = merchant.Governate,
                City = merchant.City,
                StoreName = merchant.StoreName,
                SpecialPickupCost = (int)merchant.SpecialPickupCost,
                InCompleteShippingRatio = (int)merchant.InCompleteShippingRatio,
                BranchName = merchant.Branch?.Name,

                SpecialPrices = merchant.SpecialPrices.Select(sp => new SpecialPriceDTO
                {
                    TransportCost = sp.TransportCost,
                    Governate = sp.Governate,
                    City = sp.City
                }).ToList()
            };

            return Ok(merchantDTO);
        }

        // POST: api/Merchants
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<MerchantResponseDTO>> PostMerchant(MerchantDTO merchantDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var merchant = new Merchant
            {
                FullName = merchantDTO.FullName,
                Email = merchantDTO.Email,
              //  PasswordHash = merchantDTO.Password,
                PhoneNumber = merchantDTO.PhoneNumber,
                UserName = merchantDTO.UserName,
                Address = merchantDTO.Address,
                Governate = merchantDTO.Governate,
                City = merchantDTO.City,
                StoreName = merchantDTO.StoreName,
                SpecialPickupCost = merchantDTO.SpecialPickupCost,
                InCompleteShippingRatio = merchantDTO.InCompleteShippingRatio
            };
            var result = await _userManager.CreateAsync(merchant, merchantDTO.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Assign role (if you have roles to assign)
            await _userManager.AddToRoleAsync(merchant, "merchant");

            var branch = await _context.Branches.FirstOrDefaultAsync(b => b.Name == merchantDTO.BranchName);
            if (branch == null)
            {
                branch = new Branch { Name = merchantDTO.BranchName };
                _context.Branches.Add(branch);
                await _context.SaveChangesAsync();
            }

            merchant.Branch = branch;

            foreach (var specialPriceDTO in merchantDTO.SpecialPrices)
            {
                var specialPrice = new SpecialPrice
                {
                    TransportCost = specialPriceDTO.TransportCost,
                    Governate = specialPriceDTO.Governate,
                    City = specialPriceDTO.City
                };
                merchant.SpecialPrices.Add(specialPrice);
            }

          //  _context.Merchants.Add(merchant);
            await _context.SaveChangesAsync();

            var createdMerchantDTO = new MerchantResponseDTO
            {
                Id = merchant.Id,
                FullName = merchant.FullName,
                Address = merchant.Address,
                Governate = merchant.Governate,
                City = merchant.City,
                StoreName = merchant.StoreName,
                SpecialPickupCost = (int)merchant.SpecialPickupCost,
                InCompleteShippingRatio = (int)merchant.InCompleteShippingRatio,
                BranchName = merchant.Branch?.Name,
                SpecialPrices = merchant.SpecialPrices.Select(sp => new SpecialPriceDTO
                {
                    TransportCost = sp.TransportCost,
                    Governate = sp.Governate,
                    City = sp.City
                }).ToList()
            };

            return CreatedAtAction(nameof(GetMerchant), new { id = merchant.Id }, merchantDTO);
        }

        // PUT: api/Merchants/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMerchant(string id, MerchantDTO merchantDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var merchant = await _context.Merchants
                .Include(m => m.SpecialPrices)
                .Include(m => m.Branch)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (merchant == null)
            {
                return NotFound();
            }

            merchant.FullName = merchantDTO.FullName;
            merchant.UserName = merchantDTO.UserName;
            merchant.Email = merchantDTO.Email;
            merchant.PasswordHash = merchantDTO.Password;
            merchant.PhoneNumber = merchantDTO.PhoneNumber;
            merchant.Address = merchantDTO.Address;
            merchant.Governate = merchantDTO.Governate;
            merchant.City = merchantDTO.City;
            merchant.StoreName = merchantDTO.StoreName;
            merchant.SpecialPickupCost = merchantDTO.SpecialPickupCost;
            merchant.InCompleteShippingRatio = merchantDTO.InCompleteShippingRatio;

            var branch = await _context.Branches.FirstOrDefaultAsync(b => b.Name == merchantDTO.BranchName);
            if (branch == null)
            {
                branch = new Branch { Name = merchantDTO.BranchName };
                _context.Branches.Add(branch);
                await _context.SaveChangesAsync();
            }

            merchant.Branch = branch;

            merchant.SpecialPrices.Clear();
            foreach (var specialPriceDTO in merchantDTO.SpecialPrices)
            {
                var specialPrice = new SpecialPrice
                {
                    TransportCost = specialPriceDTO.TransportCost,
                    Governate = specialPriceDTO.Governate,
                    City = specialPriceDTO.City
                };
                merchant.SpecialPrices.Add(specialPrice);
            }

            _context.Entry(merchant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MerchantExists(id))
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

        private bool MerchantExists(string id)
        {
            return _context.Merchants.Any(e => e.Id == id);
        }

        // DELETE: api/Merchants/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMerchant(string id)
        //{
        //    var merchant = await _context.Merchants.FindAsync(id);

        //    if (merchant == null)
        //    {
        //        return NotFound();
        //    }

        //    merchant.IsDeleted = true; // Mark as deleted
        //    _context.Entry(merchant).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
    }
}
