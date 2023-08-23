namespace Jaspet.DAL.Concrete;

using Abstract;
using DataManagement;
using Entity.Poco;
using Microsoft.EntityFrameworkCore;

public class EfCategoryRepository : EfRepository<Category>, ICategoryRepository
{
    public EfCategoryRepository(DbContext dbContext) : base(dbContext)
    {
    }
}