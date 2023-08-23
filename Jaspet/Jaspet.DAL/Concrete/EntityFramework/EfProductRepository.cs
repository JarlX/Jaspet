namespace Jaspet.DAL.Concrete;

using Abstract;
using DataManagement;
using Entity.Poco;
using Microsoft.EntityFrameworkCore;

public class EfProductRepository : EfRepository<Product>, IProductRepository
{
    public EfProductRepository(DbContext dbContext) : base(dbContext)
    {
    }
}