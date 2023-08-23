namespace Jaspet.DAL.Concrete.DataManagement;

using System.Linq.Expressions;
using Abstract.DataManagement;
using Entity.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public class EfRepository<T> : IRepository<T> where T : AuditableEntity
{
    private readonly DbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public EfRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> filter, params string[] includeParameters)
    {
        IQueryable<T> queryable = _dbSet;
        queryable = queryable.Where(filter);

        if (includeParameters.Length > 0)
            foreach (var item in includeParameters)
                queryable.Include(item);

        return await queryable.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null,
        params string[] includeParameters)
    {
        IQueryable<T> queryable = _dbSet;

        if (filter != null) queryable = queryable.Where(filter);

        if (includeParameters.Length > 0)
            foreach (var item in includeParameters)
                queryable.Include(item);

        return await Task.Run(() => queryable);
    }

    public async Task<EntityEntry<T>> AddAsync(T entity)
    {
        return await _dbSet.AddAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        await Task.Run(() => _dbSet.Update(entity));
    }

    public async Task RemoveAsync(T entity)
    {
        await Task.Run(() => _dbSet.Remove(entity));
    }
}