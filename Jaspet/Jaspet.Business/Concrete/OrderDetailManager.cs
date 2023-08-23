namespace Jaspet.Business.Concrete;

using System.Linq.Expressions;
using Abstract;
using DAL.Abstract.DataManagement;
using Entity.Poco;

public class OrderDetailManager : IOrderDetailService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderDetailManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OrderDetail> GetAsync(Expression<Func<OrderDetail, bool>> filter,
        params string[] includeProperties)
    {
        return await _unitOfWork.OrderDetailRepository.GetAsync(filter, includeProperties);
    }


    public async Task<IEnumerable<OrderDetail>> GetAllAsync(Expression<Func<OrderDetail, bool>> filter,
        params string[] includeProperties)
    {
        return await _unitOfWork.OrderDetailRepository.GetAllAsync(filter, includeProperties);
    }

    public async Task<OrderDetail> AddAsync(OrderDetail entity)
    {
        await _unitOfWork.OrderDetailRepository.AddAsync(entity);
        await _unitOfWork.SaveChangeAsync();
        return entity;
    }

    public async Task<OrderDetail> UpdateAsync(OrderDetail entity)
    {
        await _unitOfWork.OrderDetailRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangeAsync();
        return entity;
    }

    public async Task<OrderDetail> RemoveAsync(OrderDetail entity)
    {
        await _unitOfWork.OrderDetailRepository.RemoveAsync(entity);
        await _unitOfWork.SaveChangeAsync();
        return entity;
    }
}