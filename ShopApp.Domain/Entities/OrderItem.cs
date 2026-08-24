using ShopApp.Domain.Common;
using ShopApp.Domain.ValueObjects;

namespace ShopApp.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; private set; }

    public Guid OrderId { get; private set; }

    public Guid ProductId { get; private set; }

    public int Quantity { get; private set; }

    public Money UnitPrice { get; private set; } = null!;

    public Money TotalPrice =>
        new(UnitPrice.Amount * Quantity, UnitPrice.Currency);

    // Navigation properties
    //public Order Order { get; private set; } = null!;
    public Product Product { get; private set; } = null!;

    private OrderItem()
    {
    }

    public static Result<OrderItem> Create(
        Guid productId,
        Money unitPrice,
        int quantity)
    {
        if (productId == Guid.Empty)
        {
            return Result<OrderItem>.Failure( new Error(
                "OrderItem.ProductId",
                "Product is required"));
        }

        if (unitPrice is null)
        {
            return Result<OrderItem>.Failure( new Error(
                "OrderItem.UnitPrice",
                "Unit price is required"));
        }

        if (unitPrice.Amount <= 0)
        {
            return Result<OrderItem>.Failure( new Error(
                "OrderItem.UnitPrice",
                "Unit price must be positive"));
        }

        if (quantity <= 0)
        {
            return Result<OrderItem>.Failure( new Error(
                "OrderItem.Quantity",
                "Quantity must be greater than zero"));
        }

        var orderItem = new OrderItem
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            UnitPrice = unitPrice,
            Quantity = quantity
        };

        return Result<OrderItem>.Success(orderItem);
    }

    public Result<OrderItem> ChangeQuantity(int quantity)
    {
        if (quantity <= 0)
        {
            return Result<OrderItem>.Failure( new Error(
                "OrderItem.Quantity",
                "Quantity must be greater than zero"));
        }

        Quantity = quantity;

        return Result<OrderItem>.Success(this);
    }
}