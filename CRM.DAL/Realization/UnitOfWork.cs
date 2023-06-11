using CRM.DAL.Interfaces;
using CRM.DAL.Realization;
using CRM.DAL.Realization.Contexts;
using CRM.Shared;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Realization
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        protected Context db;
        private IRepository<User> userRepository;
        private IRepository<Base> baseRepository;
        private IRepository<BaseItem> baseItemsRepository;
        private IRepository<BaseUser> BaseUserItemsRepository;

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new BaseRepository<User>(db);
                return userRepository;
            }
        }
        public IRepository<Base> Bases
        {
            get
            {
                if (baseRepository == null)
                    baseRepository = new BaseRepository<Base>(db);
                return baseRepository;
            }
        }
        public IRepository<BaseItem> BaseItems
        {
            get
            {
                if (baseItemsRepository == null)
                    baseItemsRepository = new BaseRepository<BaseItem>(db);
                return baseItemsRepository;
            }
        }
        public IRepository<BaseUser> BaseUsers
        {
            get
            {
                if (BaseUserItemsRepository == null)
                    BaseUserItemsRepository = new BaseRepository<BaseUser>(db);
                return BaseUserItemsRepository;
            }
        }

        public async Task Save()
        {
            await db.SaveChangesAsync();
        }
    }
}
