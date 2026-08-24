using ShopApp.Application.Abstractions.Messaging;
using ShopApp.Application.Abstractions.Persistence;
using ShopApp.Domain.Common;
using ShopApp.Domain.Entities;
using ShopApp.Domain.ValueObjects;

namespace ShopApp.Application.Features.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    string Currency) : ICommand<Guid>;
public sealed class CreateProductCommandHandler
    : ICommandHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid>> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var money = Money.Create(request.Price, request.Currency);

        if (money.IsFailure)
            return Result<Guid>.Failure(money.Error);
        var productResult = Product.Create(
            request.Name,
            request.Description,
            money.Value);
        if (productResult.IsFailure)
            return Result<Guid>.Failure(productResult.Error);
        await _productRepository.AddAsync(productResult.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<Guid>.Success(productResult.Value.Id);
    }
}