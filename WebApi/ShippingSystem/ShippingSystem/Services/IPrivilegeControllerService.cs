﻿using ShippingSystem.DTOs.Privileges;
using ShippingSystem.Models;

namespace ShippingSystem.Services
{
    public interface IPrivilegeControllerService
    {
        public Task<List<Privilege>> GetAllPrivilegesAsync(int pageNumber, int pageSize);

        public Task<Privilege?> GetPrivilegeByIdAsync(int id);

        public Task<Privilege?> GetPrivilegeByNameAsync(string privilegeName);

        public Task<Privilege?> AddPrivilegeAsync(string privilegeName);

        public Task UpdatePrivilegeAsync(Privilege privilege);

        public Task DeletePrivilegeAsync(int id);

        public Task Save();
    }
}