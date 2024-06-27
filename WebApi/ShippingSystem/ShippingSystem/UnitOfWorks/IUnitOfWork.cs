namespace ShippingSystem.UnitOfWorks
{
    public interface IUnitOfWork
    {
        Task<int> Save();
    }
}
