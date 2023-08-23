namespace Jaspet.DAL.Concrete;

using Abstract;
using DataManagement;
using Entity.Poco;
using Microsoft.EntityFrameworkCore;

public class EfOrderDetailRepository : EfRepository<OrderDetail>, IOrderDetailRepository
{
    public EfOrderDetailRepository(DbContext dbContext) : base(dbContext)
    {
    }
}