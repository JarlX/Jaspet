namespace Jaspet.Business.Concrete;

using System.Linq.Expressions;
using Abstract;
using DAL.Abstract.DataManagement;
using Entity.Poco;

public class CategoryManager : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Category> GetAsync(Expression<Func<Category, bool>> filter, params string[] includeProperties)
    {
        return await _unitOfWork.CategoryRepository.GetAsync(filter, includeProperties);
    }

    public async Task<IEnumerable<Category>> GetAllAsync(Expression<Func<Category, bool>> filter,
        params string[] includeProperties)
    {
        return await _unitOfWork.CategoryRepository.GetAllAsync(filter, includeProperties);
    }

    public async Task<Category> AddAsync(Category entity)
    {
        await _unitOfWork.CategoryRepository.AddAsync(entity);
        await _unitOfWork.SaveChangeAsync();
        return entity;
    }

    public async Task<Category> UpdateAsync(Category entity)
    {
        await _unitOfWork.CategoryRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangeAsync();
        return entity;
    }

    public async Task<Category> RemoveAsync(Category entity)
    {
        await _unitOfWork.CategoryRepository.RemoveAsync(entity);
        await _unitOfWork.SaveChangeAsync();
        return entity;
    }
}