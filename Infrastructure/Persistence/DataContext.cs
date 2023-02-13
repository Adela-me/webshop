using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;
public sealed class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    //  DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}
