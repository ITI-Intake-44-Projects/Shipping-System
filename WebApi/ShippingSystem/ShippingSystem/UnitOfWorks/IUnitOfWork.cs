using ShippingSystem.Repositories;

namespace ShippingSystem.UnitOfWorks
{
    public interface IUnitOfWork
    {
        Task<int> Save();

        public GroupRepository GroupRepository { get; }

        public PrivilegeRepository PrivilegeRepository { get; }

        public GroupPrivilegeRepository GroupPrivilegeRepository { get; }
    }
}
