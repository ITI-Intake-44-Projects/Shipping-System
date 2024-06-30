﻿using Microsoft.EntityFrameworkCore;
using ShippingSystem.Models;
using ShippingSystem.UnitOfWorks;

namespace ShippingSystem.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ShippingContext db;

        /// <summary>
        /// shipping parameterized constructor
        /// </summary>
        /// <param name="db"></param>
        public GenericRepository(ShippingContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// Add Object to Database
        /// </summary>
        /// <param name="obj"></param>
        public async Task Add(T obj)
        {
            await db.Set<T>().AddAsync(obj);
        }
        
        /// <summary>
        /// Delete By Integer Id
        /// </summary>
        /// <param name="id"></param>
        public async Task Delete(int id)
        {
            var obj = await db.Set<T>().FindAsync(id);

            db.Set<T>().Remove(obj);
        }
      
        /// <summary>
        /// Delete By String Id
        /// </summary>
        /// <param name="id"></param>
        public async Task Delete(string id)
        {
            var obj = await db.Set<T>().FindAsync(id);

            db.Set<T>().Remove(obj);
        }

        /// <summary>
        /// Delete Object
        /// </summary>
        /// <param name="obj"></param>
        public async Task Delete(T obj)
        {
            db.Set<T>().Remove(obj);
        }

        /// <summary>
        /// Get All Data
        /// </summary>
        /// <returns>Dbset<T></returns>
        public async Task<List<T>> GetAll()
        {
            return await db.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Get Object By Integer Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Object</returns>
        public async Task<T> GetById(int id)
        {
            return await db.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Get Object By String Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetById(string id)
        {
            return await db.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Edit Object Data
        /// </summary>
        /// <param name="obj"></param>
        public async Task Update(T obj)
        {
            await Task.Run(() => { 
                db.Attach(obj);
                db.Entry<T>(obj).State = EntityState.Modified; 
            });
        }

        /// <summary>
        /// Save Changes to Database
        /// </summary>
        /// <returns>Number of Affected Rows</returns>
        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }
    }
}
