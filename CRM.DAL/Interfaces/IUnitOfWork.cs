using CRM.Shared;

namespace CRM.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Base> Bases { get; }
        IRepository<BaseItem> BaseItems { get; }
        IRepository<BaseUser> BaseUsers { get; }

        Task Save();
    }
}
