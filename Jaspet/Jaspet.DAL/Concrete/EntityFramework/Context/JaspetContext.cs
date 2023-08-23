namespace Jaspet.DAL.Concrete.EntityFramework.Context;

using Mapping;
using Microsoft.EntityFrameworkCore;

public class JaspetContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer(
                "Data Source=localhost; Initial Catalog = Jaspet;User ID=SA;Password=reallyStrongPwd123;TrustServerCertificate = true");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductMap());
        modelBuilder.ApplyConfiguration(new OrderDetailMap());
        modelBuilder.ApplyConfiguration(new OrderMap());
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new CategoryMap());
        base.OnModelCreating(modelBuilder);
    }
}