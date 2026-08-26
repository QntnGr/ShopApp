using MediatR;
using ShopApp.Domain.Common;
using ShopApp.Domain.Entities;

namespace ShopApp.Application.Features.Products.Queries.GetProductById;

public sealed record GetProductByIdQuery(Guid Id)
    : IRequest<Result<Product>>;