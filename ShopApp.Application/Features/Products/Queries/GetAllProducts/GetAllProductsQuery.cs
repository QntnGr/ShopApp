using MediatR;
using ShopApp.Domain.Common;
using ShopApp.Domain.Entities;

namespace ShopApp.Application.Features.Products.Queries.GetAllProducts;

public sealed record GetAllProductsQuery()
    : IRequest<Result<IReadOnlyList<Product>>>;