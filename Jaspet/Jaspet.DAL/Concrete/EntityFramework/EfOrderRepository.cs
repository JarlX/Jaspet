namespace Jaspet.DAL.Concrete;

using Abstract;
using DataManagement;
using Entity.Poco;
using Microsoft.EntityFrameworkCore;

public class EfOrderRepository : EfRepository<Order>, IOrderRepository
{
    public EfOrderRepository(DbContext dbContext) : base(dbContext)
    {
    }
}