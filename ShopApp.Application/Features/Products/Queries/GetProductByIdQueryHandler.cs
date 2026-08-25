using MediatR;
using ShopApp.Application.Abstractions.Persistence;
using ShopApp.Domain.Common;
using ShopApp.Domain.Entities;

namespace ShopApp.Application.Features.Products.Queries.GetProductById;

public sealed class GetProductByIdQueryHandler
    : IRequestHandler<GetProductByIdQuery, Result<Product>>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(
        IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<Product>> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (product is null)
        {
            return Result<Product>.Failure(
                new Error(
                    "Product.NotFound",
                    $"Product with id '{request.Id}' was not found."));
        }

        return Result<Product>.Success(product);
    }
}