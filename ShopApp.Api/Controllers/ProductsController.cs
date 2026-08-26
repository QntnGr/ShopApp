using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Api.Contracts.Products;
using ShopApp.Application.Features.Products.Commands.CreateProduct;
using ShopApp.Application.Features.Products.Queries.GetAllProducts;
using ShopApp.Application.Features.Products.Queries.GetProductById;

namespace ShopApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ISender _mediator;
    public ProductsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var query = new GetAllProductsQuery();
        var result = await _mediator.Send(query, ct);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var query = new GetProductByIdQuery(id);
        var result = await _mediator.Send(query, ct);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateProductRequest request,
        CancellationToken ct)
    {
        var command = new CreateProductCommand(
            request.Name,
            request.Description,
            request.Price,
            request.Currency);
        var result = await _mediator.Send(command, ct);
        return result.IsSuccess
            ? CreatedAtAction(
                nameof(GetById),
                new { id = result.Value },
                result.Value)
            : BadRequest(result.Error);
    }
}