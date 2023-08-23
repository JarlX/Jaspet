namespace Jaspet.DAL.Abstract.DataManagement;

using System.Linq.Expressions;
using Entity.Base;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public interface IRepository<T> where T : AuditableEntity
{
    Task<T> GetAsync(Expression<Func<T, bool>> filter, params string[] includeParameters);

    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, params string[] includeParameters);

    Task<EntityEntry<T>> AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task RemoveAsync(T entity);
}