namespace Jaspet.DAL.Abstract.DataManagement;

public interface IUnitOfWork
{
    ICategoryRepository CategoryRepository { get; }

    IOrderDetailRepository OrderDetailRepository { get; }

    IProductRepository ProductRepository { get; }

    IOrderRepository OrderRepository { get; }

    IUserRepository UserRepository { get; }

    Task<int> SaveChangeAsync();
}