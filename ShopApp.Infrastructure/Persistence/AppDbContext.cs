using Microsoft.EntityFrameworkCore;
using ShopApp.Application.Abstractions.Persistence;
using ShopApp.Domain.Entities;

namespace ShopApp.Infrastructure.Persistence;

public class AppDbContext : DbContext, IUnitOfWork
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
    public DbSet<Product> Products => Set<Product>();
    public DbSet<OrderItem> Orders => Set<OrderItem>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Auto-discover configurations
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(InfrastructureAssemblyMarker).Assembly);

        base.OnModelCreating(modelBuilder);
    }
    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}