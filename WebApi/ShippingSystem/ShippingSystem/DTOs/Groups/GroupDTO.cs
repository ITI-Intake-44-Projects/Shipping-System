﻿namespace ShippingSystem.DTOs.Groups
{
    public class GroupDTO
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
    }
}