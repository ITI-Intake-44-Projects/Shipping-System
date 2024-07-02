﻿
using Microsoft.AspNetCore.Authorization;
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
    //[Authorize(Roles = "Admin")]
    public class MerchantsController : ControllerBase
    {
        private readonly ShippingContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MerchantsController(ShippingContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Merchants
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MerchantResponseDTO>>> GetMerchants(int page = 1, int pageSize = 10)
        {
            var merchants = await _context.Merchants
                .Include(m => m.SpecialPrices)
                    .ThenInclude(sp => sp.Governate)
                .Include(m => m.SpecialPrices)
                    .ThenInclude(sp => sp.City)
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
                    Governate = sp.Governate?.Name, // Assuming Governate has Name property
                    City = sp.City?.Name // Assuming City has Name property
                }).ToList(),
                isDeleted = merchant.IsDeleted
            }).ToList();

            return Ok(merchantDTOs);
        }

        // GET: api/Merchants/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MerchantResponseDTO>> GetMerchant(string id)
        {
            var merchant = await _context.Merchants
                .Include(m => m.SpecialPrices)
                    .ThenInclude(sp => sp.Governate)
                .Include(m => m.SpecialPrices)
                    .ThenInclude(sp => sp.City)
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
                    Governate = sp.Governate?.Name, // Assuming Governate has Name property
                    City = sp.City?.Name // Assuming City has Name property
                }).ToList(),
                isDeleted = merchant.IsDeleted
            };

            return Ok(merchantDTO);
        }

        // POST: api/Merchants
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

            await _userManager.AddToRoleAsync(merchant, "merchant");

            var branch = await _context.Branches.FirstOrDefaultAsync(b => b.Name == merchantDTO.BranchName);
            if (branch == null)
            {
                branch = new Branch { Name = merchantDTO.BranchName ,AddingDate = DateTime.Now };
                _context.Branches.Add(branch);
                await _context.SaveChangesAsync();
            }

            merchant.Branch = branch;

            foreach (var specialPriceDTO in merchantDTO.SpecialPrices)
            {
                var governate = await _context.Governates.FirstOrDefaultAsync(g => g.Name == specialPriceDTO.Governate);
                if (governate == null)
                {
                    governate = new Governate { Name = specialPriceDTO.Governate };
                    _context.Governates.Add(governate);
                }

                var city = await _context.Cities.FirstOrDefaultAsync(c => c.Name == specialPriceDTO.City);
                if (city == null)
                {
                    city = new City
                    {
                        Name = specialPriceDTO.City,
                        Governate = governate
                    };
                    _context.Cities.Add(city);
                }

                var specialPrice = new SpecialPrice
                {
                    TransportCost = specialPriceDTO.TransportCost,
                    Governate = governate,
                    City = city
                };
                merchant.SpecialPrices.Add(specialPrice);
            }

            await _context.SaveChangesAsync();

            var createdMerchantDTO = new MerchantResponseDTO
            {
                Id = merchant.Id,
                FullName = merchant.FullName,
                Email = merchant.Email,
                PhoneNumber = merchant.PhoneNumber,
                UserName = merchant.UserName,
                Address = merchant.Address,
                Governate = merchant.Governate,
                City = merchant.City,
                StoreName = merchant.StoreName,
                SpecialPickupCost = (int)merchant.SpecialPickupCost,
                InCompleteShippingRatio = (int)merchant.InCompleteShippingRatio,
                BranchName = merchant.Branch?.Name,
                SpecialPrices = merchant.SpecialPrices.Select(sp => new SpecialPriceDTO
                {
                    TransportCost = (int)sp.TransportCost,
                    Governate = sp.Governate?.Name, 
                    City = sp.City?.Name 
                }).ToList(),
                isDeleted = merchant.IsDeleted
            };

            return CreatedAtAction(nameof(GetMerchant), new { id = merchant.Id }, createdMerchantDTO);
        }

        // PUT: api/Merchants/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
            merchant.PasswordHash = merchantDTO.Password; // Consider removing this line if not needed in update
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
                var governate = await _context.Governates.FirstOrDefaultAsync(g => g.Name == specialPriceDTO.Governate);
                if (governate == null)
                {
                    governate = new Governate { Name = specialPriceDTO.Governate };
                    _context.Governates.Add(governate);
                }

                var city = await _context.Cities.FirstOrDefaultAsync(c => c.Name == specialPriceDTO.City);
                if (city == null)
                {
                    city = new City
                    {
                        Name = specialPriceDTO.City,
                        Governate = governate
                    };
                    _context.Cities.Add(city);
                }

                var specialPrice = new SpecialPrice
                {
                    TransportCost = specialPriceDTO.TransportCost,
                    Governate = governate,
                    City = city
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

        // DELETE: api/Merchants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMerchant(string id)
        {
            var merchant = await _context.Merchants.FindAsync(id);

            if (merchant == null)
            {
                return NotFound();
            }

            merchant.IsDeleted = true; // Mark as deleted
            _context.Entry(merchant).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MerchantExists(string id)
        {
            return _context.Merchants.Any(e => e.Id == id);
        }
    }
}
