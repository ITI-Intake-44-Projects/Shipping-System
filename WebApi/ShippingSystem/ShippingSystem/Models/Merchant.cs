using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingSystem.Models
{
    public class Merchant : ApplicationUser
    {
        //public string? FullName { get; set; }

        public string? Address { get; set; }

        public string? Governate { get; set; }

        public string? City { get; set; }

        public string? StoreName { get; set; }

        public int? SpecialPickupCost { get; set; }

        public int? InCompleteShippingRatio { get; set; }


        [ForeignKey("Branch")]
        public int? Branch_Id { get; set; }

        public virtual Branch? Branch { get; set; }

        //public virtual ICollection<Privilege>? Privileges { get; set; } = new List<Privilege>();

        public virtual ICollection<SpecialPrice>? SpecialPrices { get; set; } = new List<SpecialPrice>();


    }
}
