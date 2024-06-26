namespace ShippingSystem.Repositories
{
    public interface IRepository<T> where T : class
    {
        public List<T> GetAll();

        public T GetById(int id);

        public void Add(T obj);

        public void Update(T obj);

        public void Delete(int id);

        public void Delete(T obj);

        public void Save();

    }
}
