
namespace ShopApp.Domain.Exceptions;

public class InsufficientStockException : Exception
{
    public InsufficientStockException(Guid productId, int requestedQuantity)
        : base($"Insufficient stock for product {productId}. Requested: {requestedQuantity}")
    {
    }
}
