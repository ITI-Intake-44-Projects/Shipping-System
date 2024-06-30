using Microsoft.EntityFrameworkCore;
using ShippingSystem.Models;
using ShippingSystem.UnitOfWorks;

namespace ShippingSystem.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ShippingContext db;

        public GenericRepository(ShippingContext db)
        {
            this.db = db;
        }

        public async Task<List<T>> GetAll()
        {
            return await db.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await db.Set<T>().FindAsync(id);
        }

        public async Task<T> GetById(string id)
        {
            return await db.Set<T>().FindAsync(id);
        }

        public async Task Add(T obj)
        {
            await db.Set<T>().AddAsync(obj);
        }

        public async Task Update(T obj)
        {
            await Task.Run(() => { 
                db.Attach(obj);
                db.Entry<T>(obj).State = EntityState.Modified; 
            });
        }

        public async Task Delete(int id)
        {
            var obj = await db.Set<T>().FindAsync(id);

            db.Set<T>().Remove(obj);
        }

        public async Task Delete(string id)
        {
            var obj = await db.Set<T>().FindAsync(id);

            db.Set<T>().Remove(obj);
        }

        public async Task Delete(T obj)
        {
            db.Set<T>().Remove(obj);
        }

        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }
    }
}