using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Persistence.DbPersistance;

public class CoreContext : DbContext
{
    public DbSet<UserEntity> Users => Set<UserEntity>();

    public CoreContext(DbContextOptions<CoreContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}