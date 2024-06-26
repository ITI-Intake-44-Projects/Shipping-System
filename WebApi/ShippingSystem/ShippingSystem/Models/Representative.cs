﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingSystem.Models
{
    public class Representative : ApplicationUser
    {
        //public string? FullName { get; set; }

        public string? Address { get; set; }

        public int? CompanyOrderPrecentage { get; set; }

        public int? SalePrecentage { get; set; }

        [ForeignKey("Branch")]
        public int? Branch_Id { get; set; }

        public virtual Branch? Branch { get; set; }

        public virtual ICollection<Privilege>? Privileges { get; set; } = new List<Privilege>();

        public virtual ICollection<RepresentativeGovernate> RepresentativeGovernates { get; set; } = new List<RepresentativeGovernate>();

    }
}
