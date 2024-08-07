﻿using Microsoft.AspNetCore.Identity;

namespace ShippingSystem.Models
{
    public class Group: IdentityRole
    {
        public  DateTime? DateAdded { get; set; }

        public virtual ICollection<GroupPrivilege> Privileges { get; set; } = new List<GroupPrivilege>();

    }
}
