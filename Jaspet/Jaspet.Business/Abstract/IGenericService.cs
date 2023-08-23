namespace Jaspet.Business.Abstract;

using System.Linq.Expressions;

public interface IGenericService<T>
{
    Task<T> GetAsync(Expression<Func<T, bool>> filter, params string[] includeProperties);

    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, params string[] includeProperties);

    Task<T> AddAsync(T entity);

    Task<T> UpdateAsync(T entity);

    Task<T> RemoveAsync(T entity);
}