namespace Jaspet.Business.Concrete;

using System.Linq.Expressions;
using Abstract;
using DAL.Abstract.DataManagement;
using Entity.Poco;

public class UserManager : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<User> GetAsync(Expression<Func<User, bool>> filter, params string[] includeProperties)
    {
        return await _unitOfWork.UserRepository.GetAsync(filter, includeProperties);
    }

    public async Task<IEnumerable<User>> GetAllAsync(Expression<Func<User, bool>> filter,
        params string[] includeProperties)
    {
        return await _unitOfWork.UserRepository.GetAllAsync(filter, includeProperties);
    }

    public async Task<User> AddAsync(User entity)
    {
        await _unitOfWork.UserRepository.AddAsync(entity);
        await _unitOfWork.SaveChangeAsync();
        return entity;
    }

    public async Task<User> UpdateAsync(User entity)
    {
        await _unitOfWork.UserRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangeAsync();
        return entity;
    }

    public async Task<User> RemoveAsync(User entity)
    {
        await _unitOfWork.UserRepository.RemoveAsync(entity);
        await _unitOfWork.SaveChangeAsync();
        return entity;
    }
}