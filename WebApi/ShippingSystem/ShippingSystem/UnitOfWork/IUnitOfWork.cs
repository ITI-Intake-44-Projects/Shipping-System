namespace ShippingSystem.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> Save();
    }
}
