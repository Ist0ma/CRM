using System.Linq.Expressions;

namespace CRM.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(int id);
        IQueryable<T> Find(Expression<Func<T, Boolean>> predicate = null);
        Task Create(T item);
        void Update(T item);
        void Update(IEnumerable<T> items);
        Task Delete(T item);
    }
}
