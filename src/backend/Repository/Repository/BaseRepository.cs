using Repository.IRepository;
using Repository.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Repository.Exceptions;
using Repository.Context;
using Serilog;

namespace Repository.Repository
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : BaseEntity
    {
        protected BaseContext _context;
        protected DbSet<T> _dbSet;

        public BaseRepository(BaseContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.AsNoTracking().SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking()
                    .Where(predicate)
                    .ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            var added = await _dbSet.AddAsync(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException)
            {
                Log.Logger.Error($"{typeof(T).Name} duplicate: id={entity.Id}");
                throw new EntityDuplicateException($"{typeof(T).Name} duplicate: id={entity.Id}");
            }

            added.State = EntityState.Detached;

            return added.Entity;
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                Log.Logger.Error($"{typeof(T).Name} duplicate in list");
                throw new EntityDuplicateException($"{typeof(T).Name} duplicate in list");
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var added = _dbSet.Update(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                Log.Logger.Error($"{typeof(T).Name} not exists: id={entity.Id}");
                throw new EntityNotExistsException($"{typeof(T).Name} not exists: id={entity.Id}");
            }

            added.State = EntityState.Detached;

            return added.Entity;
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                Log.Logger.Error($"{typeof(T).Name} not exists in list");
                throw new EntityNotExistsException($"{typeof(T).Name} not exists in list");
            }
        }

        public async Task<T> RemoveAsync(T entity)
        {
            var removed = _dbSet.Remove(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                Log.Logger.Error($"{typeof(T).Name} not exists: id={entity.Id}");
                throw new EntityNotExistsException($"{typeof(T).Name} not exists: id={entity.Id}");
            }

            return removed.Entity;
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                Log.Logger.Error($"{typeof(T).Name} not exists in list");
                throw new EntityNotExistsException($"{typeof(T).Name} not exists in list");
            }
        }
    }
}
