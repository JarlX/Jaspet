namespace Jaspet.Business.Concrete;

using System.Linq.Expressions;
using Abstract;
using DAL.Abstract.DataManagement;
using Entity.Poco;

public class OrderManager : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Order> GetAsync(Expression<Func<Order, bool>> filter, params string[] includeProperties)
    {
        return await _unitOfWork.OrderRepository.GetAsync(filter, includeProperties);
    }

    public async Task<IEnumerable<Order>> GetAllAsync(Expression<Func<Order, bool>> filter,
        params string[] includeProperties)
    {
        return await _unitOfWork.OrderRepository.GetAllAsync(filter, includeProperties);
    }

    public async Task<Order> AddAsync(Order entity)
    {
        await _unitOfWork.OrderRepository.AddAsync(entity);
        await _unitOfWork.SaveChangeAsync();
        return entity;
    }

    public async Task<Order> UpdateAsync(Order entity)
    {
        await _unitOfWork.OrderRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangeAsync();
        return entity;
    }

    public async Task<Order> RemoveAsync(Order entity)
    {
        await _unitOfWork.OrderRepository.RemoveAsync(entity);
        await _unitOfWork.SaveChangeAsync();
        return entity;
    }
}