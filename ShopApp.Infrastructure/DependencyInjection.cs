using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopApp.Application.Abstractions.Persistence;
using ShopApp.Infrastructure.Persistence;
using ShopApp.Infrastructure.Persistence.Repositories;

namespace ShopApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(
                configuration.GetConnectionString("DefaultConnection"),
                sqliteOptions =>
                {
                    sqliteOptions.MigrationsAssembly(
                        typeof(InfrastructureAssemblyMarker).Assembly.FullName);
                });

        });

        // Repositories
        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<IUnitOfWork>(sp =>
            sp.GetRequiredService<AppDbContext>());


        return services;
    }
}