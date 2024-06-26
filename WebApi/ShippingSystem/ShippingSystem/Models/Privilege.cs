using Microsoft.AspNetCore.Identity;

namespace ShippingSystem.Models
{
    public class Privilege : IdentityRole
    {
        
        public bool? Add { get; set; }

        public bool? Update { get; set; }

        public bool? View { get; set; }

        public bool? Delete { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

        public virtual ICollection<Merchant> Merchants { get; set; } = new List<Merchant>();

        public virtual ICollection<Representative> Representatives { get; set; } = new List<Representative>();

    }
}
