using System.Linq.Expressions;

namespace Repository.IRepository
{
    public interface IBaseRepository<T>
    {
        Task<T?> GetByIdAsync(int id);

        Task<List<T>> GetAllAsync();

        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);

        Task<T> AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);

        Task<T> UpdateAsync(T entity);

        Task UpdateRangeAsync(IEnumerable<T> entities);

        Task<T> RemoveAsync(T entity);

        Task RemoveRangeAsync(IEnumerable<T> entities);
    }
}
