
using ShopApp.Domain.Common;
using ShopApp.Domain.Exceptions;
using ShopApp.Domain.ValueObjects;

namespace ShopApp.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public Money Price { get; private set; } = null!;
    public int StockQuantity { get; private set; }
    public bool IsActive { get; private set; } = true;
    public DateTime CreatedAt { get; private set; }

    // Navigation property (no EF attributes!)
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
    private Product() { } // EF Core protected/private constructor support
    public static Result<Product> Create(string name, string description, Money price)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<Product>.Failure(new Error("500", "Product: Name is required"));

        if (price.Amount <= 0)
            return Result<Product>.Failure(new Error("500", "Product: Price must be positive"));
        return Result<Product>.Success(new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Price = price,
            CreatedAt = DateTime.UtcNow
        });
    }
    public void ReduceStock(int quantity)
    {
        if (quantity <= 0) throw new ArgumentException("Quantity must be positive");
        if (StockQuantity < quantity) throw new InsufficientStockException(Id, quantity);

        StockQuantity -= quantity;
    }
}