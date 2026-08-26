using Microsoft.EntityFrameworkCore;
using ShopApp.Application.Abstractions.Persistence;
using ShopApp.Domain.Entities;

namespace ShopApp.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Product?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
    }
    public void Update(Product product)
    {
        _context.Products.Update(product);
    }

    public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Take(100)
            .ToListAsync(cancellationToken);
    }

    public void Remove(Product product)
    {
        _context.Products.Remove(product);
    }
}