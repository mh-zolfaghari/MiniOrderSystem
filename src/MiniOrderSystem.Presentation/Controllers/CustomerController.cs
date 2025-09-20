using MiniOrderSystem.Application.DTOs;
using MiniOrderSystem.Application.UseCases.Customers.Commands.CreateCustomer;
using MiniOrderSystem.Application.UseCases.Customers.Queries.GetCustomerById;
using MiniOrderSystem.Application.UseCases.Customers.Queries.GetCustomers;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Presentation.Controllers
{
    public class CustomerController(ISender sender) : BaseController(sender)
    {
        [HttpPost("/api/customers")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command, CancellationToken cancellationToken = default)
            => OK(await MediatR.Send(command, cancellationToken));

        [HttpGet("/api/customers/{id:int}")]
        [ProducesResponseType(typeof(Result<CustomerDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken = default)
            => Ok(await MediatR.Send(new GetCustomerByIdQuery { Id = id }, cancellationToken));

        [HttpGet("/api/customers")]
        [ProducesResponseType(typeof(Result<IEnumerable<CustomerDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] GetCustomersQuery query, CancellationToken cancellationToken = default)
            => Ok(await MediatR.Send(query, cancellationToken));
    }
}
