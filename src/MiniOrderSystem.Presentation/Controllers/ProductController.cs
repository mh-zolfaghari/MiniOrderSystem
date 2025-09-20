using MiniOrderSystem.Application.DTOs;
using MiniOrderSystem.Application.UseCases.Products.Commands.CreateProduct;
using MiniOrderSystem.Application.UseCases.Products.Queires.GetProductById;
using MiniOrderSystem.Application.UseCases.Products.Queires.GetProducts;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Presentation.Controllers
{
    public class ProductController(ISender sender) : BaseController(sender)
    {
        [HttpPost("/api/products")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command, CancellationToken cancellationToken = default)
            => OK(await MediatR.Send(command, cancellationToken));

        [HttpGet("/api/products/{id:int}")]
        [ProducesResponseType(typeof(Result<ProductDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken = default)
            => Ok(await MediatR.Send(new GetProductByIdQuery { Id = id }, cancellationToken));

        [HttpGet("/api/products")]
        [ProducesResponseType(typeof(Result<IEnumerable<ProductDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] GetProductsQuery query, CancellationToken cancellationToken = default)
            => Ok(await MediatR.Send(query, cancellationToken));
    }
}
