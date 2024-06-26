using ShippingSystem.Models;

namespace ShippingSystem.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ShippingContext db;

        public Repository(ShippingContext _db)
        {
            db = _db;
        }
        public void Add(T obj)
        {
            db.Set<T>().Add(obj);
        }

        public void Delete(int id)
        {
            var obj = db.Set<T>().Find(id);

            db.Set<T>().Remove(obj);
        }

        public void Delete(T obj)
        {
            db.Set<T>().Remove(obj);
        }

        public List<T> GetAll()
        {
            return db.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return db.Set<T>().Find(id);
        }

        public void Update(T obj)
        {
            db.Set<T>().Update(obj);
        }

        public void Save()
        {
            db.SaveChanges();
        }

    }
}
