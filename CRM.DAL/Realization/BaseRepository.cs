using CRM.DAL.Interfaces;
using CRM.DAL.Realization.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL.Realization
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private Context db;

        public BaseRepository(Context context)
        {
            this.db = context;
        }

        public async Task Create(TEntity item)
        {
            await db.Set<TEntity>().AddAsync(item);
        }

        public async Task Delete(TEntity item)
        {
            db.Set<TEntity>().Remove(item);
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
                return db.Set<TEntity>();
            return db.Set<TEntity>().Where(predicate);
        }

        public async Task<TEntity> Get(int id)
        {
            return await db.Set<TEntity>().FindAsync(id);
        }

        public void Update(TEntity item)
        {
            db.Set<TEntity>().Update(item);
        }

        public void Update(IEnumerable<TEntity> items)
        {
            db.Set<TEntity>().UpdateRange(items);
        }
    }
}
