using MediatR;
using ShopApp.Application.Abstractions.Persistence;
using ShopApp.Domain.Common;
using ShopApp.Domain.Entities;

namespace ShopApp.Application.Features.Products.Queries.GetAllProducts;

public sealed class GetAllProductsQueryHandler(IProductRepository productRepository)
    : IRequestHandler<GetAllProductsQuery, Result<IReadOnlyList<Product>>>
{
    private readonly IProductRepository _productRepository = productRepository;


    public async Task<Result<IReadOnlyList<Product>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        return await _productRepository.GetAllAsync(cancellationToken)
            .ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    return Result<IReadOnlyList<Product>>.Failure(new Error("500", "Failed to retrieve products."));
                }
                var products = task.Result;
                return Result<IReadOnlyList<Product>>.Success(products);
            }, cancellationToken);
    }
}
