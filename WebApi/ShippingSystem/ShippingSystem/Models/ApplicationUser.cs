using Microsoft.AspNetCore.Identity;

namespace ShippingSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }

    }
}
