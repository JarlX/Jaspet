namespace Jaspet.DAL.Concrete.DataManagement;

using Abstract;
using Abstract.DataManagement;
using Entity.Base;
using EntityFramework.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly JaspetContext _jaspetContext;

    public EfUnitOfWork(JaspetContext jaspetContext, IHttpContextAccessor httpContextAccessor)
    {
        _jaspetContext = jaspetContext;
        _httpContextAccessor = httpContextAccessor;

        CategoryRepository = new EfCategoryRepository(_jaspetContext);
        OrderDetailRepository = new EfOrderDetailRepository(_jaspetContext);
        ProductRepository = new EfProductRepository(_jaspetContext);
        OrderRepository = new EfOrderRepository(_jaspetContext);
        UserRepository = new EfUserRepository(_jaspetContext);
    }

    public ICategoryRepository CategoryRepository { get; }
    public IOrderDetailRepository OrderDetailRepository { get; }
    public IProductRepository ProductRepository { get; }
    public IOrderRepository OrderRepository { get; }
    public IUserRepository UserRepository { get; }

    public async Task<int> SaveChangeAsync()
    {
        foreach (var item in _jaspetContext.ChangeTracker.Entries<AuditableEntity>())
            if (item.State == EntityState.Added)
            {
                item.Entity.CreatedTime = DateTime.UtcNow;
                item.Entity.UpdatedTime = DateTime.UtcNow;
                item.Entity.CreatedUserId = 1;
                item.Entity.UpdatedTime = DateTime.UtcNow;
                item.Entity.Guid = Guid.NewGuid();
                item.Entity.CreatedIPV4Address = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                item.Entity.UpdatedIPV4Address = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            }
            else if (item.State == EntityState.Modified)
            {
                item.Entity.UpdatedTime = DateTime.UtcNow;
                item.Entity.UpdatedUser = 1;
                item.Entity.UpdatedIPV4Address = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            }

        return await _jaspetContext.SaveChangesAsync();
    }
}