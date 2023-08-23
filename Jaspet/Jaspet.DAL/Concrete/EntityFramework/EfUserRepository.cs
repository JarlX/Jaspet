namespace Jaspet.DAL.Concrete;

using Abstract;
using DataManagement;
using Entity.Poco;
using Microsoft.EntityFrameworkCore;

public class EfUserRepository : EfRepository<User>, IUserRepository
{
    public EfUserRepository(DbContext dbContext) : base(dbContext)
    {
    }
}