namespace ShopApp.Api.Contracts.Products;

public record CreateProductRequest
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; } = decimal.Zero;
    public string Currency { get; init; } = string.Empty;
}
