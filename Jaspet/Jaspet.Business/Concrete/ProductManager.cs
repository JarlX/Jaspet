namespace Jaspet.Business.Concrete;

using System.Linq.Expressions;
using Abstract;
using DAL.Abstract.DataManagement;
using Entity.Poco;

public class ProductManager : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Product> GetAsync(Expression<Func<Product, bool>> filter, params string[] includeProperties)
    {
        return await _unitOfWork.ProductRepository.GetAsync(filter, includeProperties);
    }

    public async Task<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>> filter,
        params string[] includeProperties)
    {
        return await _unitOfWork.ProductRepository.GetAllAsync(filter, includeProperties);
    }

    public async Task<Product> AddAsync(Product entity)
    {
        await _unitOfWork.ProductRepository.AddAsync(entity);
        await _unitOfWork.SaveChangeAsync();
        return entity;
    }

    public async Task<Product> UpdateAsync(Product entity)
    {
        await _unitOfWork.ProductRepository.AddAsync(entity);
        await _unitOfWork.SaveChangeAsync();
        return entity;
    }

    public async Task<Product> RemoveAsync(Product entity)
    {
        await _unitOfWork.ProductRepository.AddAsync(entity);
        await _unitOfWork.SaveChangeAsync();
        return entity;
    }
}